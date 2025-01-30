using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;

namespace SalesInvoicesScheduler.Worker
{
    public class WorkerSalesInvoices
    {
        private readonly ILogger<WorkerSalesInvoices> _logger;

        public WorkerSalesInvoices(ILogger<WorkerSalesInvoices> logger)
        {
            _logger = logger;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation($"Worker started at: {DateTime.UtcNow}");

                var startDate = DateTime.UtcNow;
                var endDate = DateTime.UtcNow;
                var outputFilePath = "/Users/hyou/Documents/TEST_JSON/Test.json";

                await SalesInvoicesHelper.FetchAndLogSalesInvoicesAsync(
    "ySriK1ZF1hT3x5jU", // Replace with actual CLIENT_ID
    "zVbG8sai3CUM5uiWnewt5GLPFpwE4bUe", // Replace with actual CLIENT_SECRET
    outputFilePath
);

                _logger.LogInformation($"Worker completed at: {DateTime.UtcNow}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in WorkerSalesInvoices: {ex.Message}", ex);
            }
        }
    }
}
