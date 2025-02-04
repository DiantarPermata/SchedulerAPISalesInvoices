using Cronos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Worker;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SalesInvoicesScheduler.Scheduler
{
    public class SchedulerSalesInvoices : BackgroundService
    {
        private readonly ILogger<SchedulerSalesInvoices> _logger;
        private readonly WorkerSalesInvoices _worker;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private System.Timers.Timer _timer;

        public SchedulerSalesInvoices(ILogger<SchedulerSalesInvoices> logger, WorkerSalesInvoices worker, IConfiguration configuration)
        {
            _logger = logger;
            _worker = worker;
            _expression = CronExpression.Parse(configuration["Scheduler:CronSchedule"], CronFormat.IncludeSeconds); // Setiap 5 menit
            _timeZoneInfo = TimeZoneInfo.Local;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                SetNextOccurance();
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }

        private void SetNextOccurance()
        {
            _timer?.Stop();
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) => await DoWork();
                _timer.Start();
            }
        }

        private async Task DoWork()
        {
            _logger.LogInformation($"Scheduler triggered at: {DateTime.UtcNow}");
            await _worker.RunAsync();
            SetNextOccurance();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            _logger.LogInformation("Scheduler is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}