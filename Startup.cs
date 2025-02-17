using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Services;
using SalesInvoicesScheduler.Worker;
using System;

[assembly: FunctionsStartup(typeof(SalesInvoicesScheduler.Startup))]
namespace SalesInvoicesScheduler
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Konfigurasi logging dengan Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("/Users/hyou/Documents/TEST_JSON/Test.json", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog();
            });

            // Registrasi helper services
            builder.Services.AddTransient<ApiHelperService>();
            builder.Services.AddTransient<DeliveryOrderHelper>();
            builder.Services.AddTransient<DeliveryOrderService>();
            builder.Services.AddTransient<ProductService>();
            builder.Services.AddTransient<ProductHelper>();
            builder.Services.AddTransient<SalesInvoiceHelper>();
            builder.Services.AddTransient<SalesInvoiceService>();
            builder.Services.AddTransient<SalesInvoiceTagHelper>();
            builder.Services.AddTransient<SalesInvoiceTagService>();
            builder.Services.AddTransient<TransactionLineHelper>();
            builder.Services.AddTransient<TransactionLineService>();

            // Registrasi service dengan DI
            builder.Services.AddTransient<DeliveryOrderService>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var connectionString = configuration["ConnectionStrings:SQLServer"];
                var jsonFilePath = configuration["JsonFilePathDeliveryOrders"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'SQLServer' is not configured.");
                if (string.IsNullOrEmpty(jsonFilePath))
                    throw new InvalidOperationException("JsonFilePathDeliveryOrders is not configured.");
                return new DeliveryOrderService(connectionString, jsonFilePath);
            });

            builder.Services.AddTransient<ProductService>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var connectionString = configuration["ConnectionStrings:SQLServer"];
                var jsonFilePath = configuration["JsonFilePathProducts"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'SQLServer' is not configured.");
                if (string.IsNullOrEmpty(jsonFilePath))
                    throw new InvalidOperationException("JsonFilePathProducts is not configured.");
                return new ProductService(connectionString, jsonFilePath);
            });

            builder.Services.AddTransient<SalesInvoiceService>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var connectionString = configuration["ConnectionStrings:SQLServer"];
                var jsonFilePath = configuration["JsonFilePathSalesInvoices"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'SQLServer' is not configured.");
                if (string.IsNullOrEmpty(jsonFilePath))
                    throw new InvalidOperationException("JsonFilePathSalesInvoices is not configured.");
                return new SalesInvoiceService(connectionString, jsonFilePath);
            });

            builder.Services.AddTransient<SalesInvoiceTagService>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var connectionString = configuration["ConnectionStrings:SQLServer"];
                var jsonFilePath = configuration["JsonFilePathSalesInvoiceTags"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'SQLServer' is not configured.");
                if (string.IsNullOrEmpty(jsonFilePath))
                    throw new InvalidOperationException("JsonFilePathSalesInvoiceTags is not configured.");
                return new SalesInvoiceTagService(connectionString, jsonFilePath);
            });

            builder.Services.AddTransient<TransactionLineService>(sp =>
            {
                var configuration = sp.GetService<IConfiguration>();
                var connectionString = configuration["ConnectionStrings:SQLServer"];
                var jsonFilePath = configuration["JsonFilePathTransactionLines"];
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException("Connection string 'SQLServer' is not configured.");
                if (string.IsNullOrEmpty(jsonFilePath))
                    throw new InvalidOperationException("JsonFilePathTransactionLines is not configured.");
                return new TransactionLineService(connectionString, jsonFilePath);
            });

            // Registrasi worker
            builder.Services.AddTransient<DeliveryOrderWorker>();
            builder.Services.AddTransient<ProductWorker>();
            builder.Services.AddTransient<SalesInvoiceWorker>();
            builder.Services.AddTransient<SalesInvoiceTagWorker>();
            builder.Services.AddTransient<TransactionLineWorker>();
        }
    }
}
