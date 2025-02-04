using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ApplicationInsights;
using SalesInvoicesScheduler.Helpers.HelperService;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Worker;
using SalesInvoicesScheduler.Scheduler;

namespace SalesInvoicesScheduler
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Tambahkan Application Insights untuk telemetry (opsional)
                    services.AddApplicationInsightsTelemetryWorkerService();

                    // Registrasi helper
                    services.AddTransient<HmacHelperService>();
                    services.AddTransient<ApiHelperService>();
                    services.AddTransient<SalesInvoicesHelper>();

                    // Registrasi worker
                    services.AddSingleton<WorkerSalesInvoices>();

                    // Registrasi scheduler
                    services.AddHostedService<SchedulerSalesInvoices>();
                })
                .UseWindowsService(); // Gunakan ini jika aplikasi dijalankan sebagai Windows Service
    }
}