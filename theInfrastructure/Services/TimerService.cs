
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace theInfrastructure
{
    public class ScopedTimerService : IDisposable
    {
        private readonly Dictionary<string, RegistrationEntry> _registrations = new();
        private readonly object _lock = new();
        private readonly CancellationTokenSource _cts = new();
        private ulong _executionCycle = 0;

        private class RegistrationEntry
        {
            public Func<Task> Callback { get; set; } = default!;
            public UpdateInterval Interval { get; set; }
        }

        public ScopedTimerService()
        {
            // Verwendung von PeriodicTimer statt System.Threading.Timer zur Vermeidung von Überlappungen
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
                    catch (Exception ex)
                    {
                        
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
            using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

            while (!cancellationToken.IsCancellationRequested && await timer.WaitForNextTickAsync(cancellationToken))
            {
                List<Func<Task>> tasksToRun = new();

                lock (_lock)
                {
                    unchecked
                    { _executionCycle++; }

                    foreach (var entry in _registrations.Values)
                    {
                        if (_executionCycle % (ulong)entry.Interval == 0)
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
                    catch (Exception ex)
                    {
                        
                    }
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