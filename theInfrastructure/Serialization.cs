using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace theInfrastructure
{
    public static class Serializer
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        static string filepath = "./Configuration/";

        public static void Save<T>(T instance, string filename) where T : class
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            try
            {
                string json = ToJsonString(instance);
                string crypto = Encryption.Encrypt(json);

                File.WriteAllText(filepath + filename, crypto);
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                throw;
            }
        }
        public static T Load<T>(string filename) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(filename))
                throw new ArgumentException("Dateipfad darf nicht leer sein.", nameof(filename));

            try
            {
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                if (!File.Exists(filepath + filename))
                {
                    T defaultInstance = new T();
                    Save(defaultInstance, filename);
                    return defaultInstance;
                }

                string crypto = File.ReadAllText(filepath + filename);
                string json = Encryption.Decrypt(crypto);

                T obj = ToObjectFromJson<T>(json);
                return obj ?? new T();
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                return new T();
            }
        }

        private static string ToJsonString<T>(T theObject)
        {
            try
            {

                return JsonSerializer.Serialize(theObject, JsonOptions);
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                return string.Empty;
            }
        }
        private static T ToObjectFromJson<T>(string json) where T : class, new()
        {
            if (string.IsNullOrEmpty(json))
            {
                return new T();
            }

            try
            {

                return JsonSerializer.Deserialize<T>(json, JsonOptions) ?? new T();
            }
            catch (Exception ex)
            {
                //Helper.LogEx(ex);
                return new T();
            }
        }
    }



    public interface IExceptionLogger
    {
        bool TryLog(Exception exception);
    }

    public sealed class AsyncExceptionLogger : BackgroundService, IExceptionLogger
    {
        private const int ChannelCapacity = 16_384;
        private const int MaxBatchEntries = 1_024;
        private const int BatchDelayMilliseconds = 50;

        private readonly Channel<ExceptionLogEntry> _channel;
        private readonly string _logFilePath;

        private long _written;
        private long _dropped;
        private long _writeErrors;

        public AsyncExceptionLogger(IHostEnvironment environment)
        {
            var logDirectory = Path.Combine(environment.ContentRootPath, "Logs");

            Directory.CreateDirectory(logDirectory);

            _logFilePath = Path.Combine(logDirectory, "exceptions.log");

            _channel = Channel.CreateBounded<ExceptionLogEntry>(
                new BoundedChannelOptions(ChannelCapacity)
                {
                    SingleReader = true,
                    SingleWriter = false,

                    // Der Logger darf den aufrufenden Thread niemals blockieren.
                    FullMode = BoundedChannelFullMode.DropWrite,

                    // Bei hoher Last TryWrite direkt verwenden.
                    AllowSynchronousContinuations = false
                });
        }

        public bool TryLog(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            var entry = new ExceptionLogEntry(
                DateTime.UtcNow,
                exception);

            if (_channel.Writer.TryWrite(entry))
            {
                return true;
            }

            Interlocked.Increment(ref _dropped);
            return false;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            await using var fileStream = new FileStream(
                _logFilePath,
                FileMode.Append,
                FileAccess.Write,
                FileShare.Read,
                bufferSize: 64 * 1024,
                options: FileOptions.Asynchronous | FileOptions.SequentialScan);

            await using var writer = new StreamWriter(
                fileStream,
                new UTF8Encoding(encoderShouldEmitUTF8Identifier: false),
                bufferSize: 64 * 1024);

            try
            {
                await ProcessQueueAsync(writer, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                // Normaler Host-Shutdown.
            }
            finally
            {
                // Bereits eingereihtes Material noch verarbeiten.
                await DrainQueueAsync(writer);
            }

            await writer.FlushAsync();
        }

        private async Task ProcessQueueAsync(
            StreamWriter writer,
            CancellationToken stoppingToken)
        {
            var reader = _channel.Reader;

            while (await reader.WaitToReadAsync(stoppingToken))
            {
                var count = 0;

                // Ersten verfügbaren Eintrag verarbeiten.
                while (count < MaxBatchEntries &&
                       reader.TryRead(out var entry))
                {
                    WriteEntry(writer, entry);
                    count++;
                }

                if (count == 0)
                    continue;

                // Kurzes Coalescing-Fenster:
                // weitere Einträge kommen mit in denselben I/O-Batch.
                if (BatchDelayMilliseconds > 0)
                {
                    try
                    {
                        await Task.Delay(
                            BatchDelayMilliseconds,
                            stoppingToken);
                    }
                    catch (OperationCanceledException)
                        when (stoppingToken.IsCancellationRequested)
                    {
                        break;
                    }
                }

                while (count < MaxBatchEntries &&
                       reader.TryRead(out var entry))
                {
                    WriteEntry(writer, entry);
                    count++;
                }

                await writer.FlushAsync(stoppingToken);

                Interlocked.Add(ref _written, count);
            }
        }

        private async Task DrainQueueAsync(StreamWriter writer)
        {
            var reader = _channel.Reader;

            var count = 0;

            while (reader.TryRead(out var entry))
            {
                WriteEntry(writer, entry);
                count++;

                if (count >= MaxBatchEntries)
                {
                    await writer.FlushAsync();
                    Interlocked.Add(ref _written, count);
                    count = 0;
                }
            }

            if (count > 0)
            {
                await writer.FlushAsync();
                Interlocked.Add(ref _written, count);
            }
        }

        private static void WriteEntry(
            StreamWriter writer,
            in ExceptionLogEntry entry)
        {
            writer.Write('[');
            writer.Write(entry.Timestamp.ToString(
                "yyyy-MM-dd HH:mm:ss.fff"));
            writer.Write("] ");

            WriteException(writer, entry.Exception);

            writer.WriteLine();
            writer.WriteLine("---");
        }

        private static void WriteException(
            StreamWriter writer,
            Exception exception)
        {
            writer.Write(exception.GetType().FullName);
            writer.Write(": ");
            writer.WriteLine(exception.Message);

            if (!string.IsNullOrEmpty(exception.StackTrace))
            {
                writer.WriteLine(exception.StackTrace);
            }

            var inner = exception.InnerException;

            while (inner is not null)
            {
                writer.Write(" ---> ");
                writer.Write(inner.GetType().FullName);
                writer.Write(": ");
                writer.WriteLine(inner.Message);

                if (!string.IsNullOrEmpty(inner.StackTrace))
                {
                    writer.WriteLine(inner.StackTrace);
                }

                inner = inner.InnerException;
            }
        }

        public override async Task StopAsync(
            CancellationToken cancellationToken)
        {
            // Keine neuen Einträge mehr akzeptieren.
            _channel.Writer.TryComplete();

            await base.StopAsync(cancellationToken);
        }

        public LoggerStatistics GetStatistics()
        {
            return new LoggerStatistics(
                Interlocked.Read(ref _written),
                Interlocked.Read(ref _dropped),
                Interlocked.Read(ref _writeErrors));
        }

        private readonly record struct ExceptionLogEntry(
            DateTime Timestamp,
            Exception Exception);
    }

    public readonly record struct LoggerStatistics(
        long Written,
        long Dropped,
        long WriteErrors);
}
