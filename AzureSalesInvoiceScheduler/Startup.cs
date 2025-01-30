using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using SalesInvoicesScheduler.Worker;
using Serilog;

[assembly: FunctionsStartup(typeof(SalesInvoicesScheduler.Startup))]

namespace SalesInvoicesScheduler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("/Users/hyou/Documents/TEST_JSON/Test.json", rollingInterval: RollingInterval.Day, outputTemplate: "{Message:lj}{NewLine}")
                .CreateLogger();

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

            builder.Services.AddTransient<SalesInvoicesFunction>();
            builder.Services.AddTransient<WorkerSalesInvoices>();
        }
    }
}
