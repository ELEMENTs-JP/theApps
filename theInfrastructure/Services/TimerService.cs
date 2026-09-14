using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace theInfrastructure
{
    public class TimerService : IDisposable
    {
        private readonly Dictionary<string, RegistrationEntry> _registrations = new();
        private readonly object _lock = new();
        private readonly CancellationTokenSource _cts = new();

        private class RegistrationEntry
        {
            public Func<Task> Callback { get; set; } = default!;
            public UpdateInterval Interval { get; set; }
        }

        public TimerService()
        {
            Task.Run(() => StartTimerAsync(_cts.Token));
        }

        public void Register(string key, UpdateInterval interval, Func<Task> callback, bool runImmediately = false)
        {
            lock (_lock)
            {
                _registrations[key] = new RegistrationEntry
                {
                    Callback = callback,
                    Interval = interval
                };
            }

            if (runImmediately)
            {
                _ = Task.Run(async () =>
                {
                    try
                    {
                        await callback();
                    }
                    catch (Exception)
                    {
                        // Logging hier ergänzen
                    }
                });
            }
        }

        public void Unregister(string key)
        {
            lock (_lock)
            {
                _registrations.Remove(key);
            }
        }

        private async Task StartTimerAsync(CancellationToken cancellationToken)
        {
            // 1. Synchronisierung auf die nächste volle Minute (HH:MM:00.000)
            var now = DateTime.Now;
            var nextMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, now.Kind).AddMinutes(1);
            var initialDelay = nextMinute - now;

            try
            {
                await Task.Delay(initialDelay, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            // 2. PeriodicTimer startet exakt zur vollen Minute
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            // Erste Ausführung direkt nach Ablauf des initialen Delays für Minute 00
            await ProcessTickAsync();

            while (!cancellationToken.IsCancellationRequested && await timer.WaitForNextTickAsync(cancellationToken))
            {
                await ProcessTickAsync();
            }
        }

        private async Task ProcessTickAsync()
        {
            List<Func<Task>> tasksToRun = new();
            int currentMinute = DateTime.Now.Minute;

            lock (_lock)
            {
                foreach (var entry in _registrations.Values)
                {
                    // 3. Auswertung gegen die reale Systemminute statt eines lokalen Zählers
                    if (currentMinute % (int)entry.Interval == 0)
                    {
                        tasksToRun.Add(entry.Callback);
                    }
                }
            }

            foreach (var task in tasksToRun)
            {
                try
                {
                    await task();
                }
                catch (Exception)
                {
                    // Logging hier ergänzen
                }
            }
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();

            lock (_lock)
            {
                _registrations.Clear();
            }
        }
    }

    public enum UpdateInterval
    {
        OneMinute = 1,
        TwoMinutes = 2,
        FiveMinutes = 5,
        TenMinutes = 10,
        SixtyMinutes = 60
    }
}