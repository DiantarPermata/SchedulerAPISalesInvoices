using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SalesInvoicesScheduler.Worker
{
    public class WorkerSalesInvoices
    {
        private readonly ILogger _logger;
        private readonly SalesInvoicesScheduler.Helpers.SalesInvoicesHelper _helper;

        public WorkerSalesInvoices(ILogger<WorkerSalesInvoices> logger, SalesInvoicesScheduler.Helpers.SalesInvoicesHelper helper)
        {
            _logger = logger;
            _helper = helper;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation($"Worker started at: {DateTime.UtcNow}");

                await _helper.FetchAndLogSalesInvoicesAsync(
                    "ySriK1ZF1hT3x5jU", // Replace with actual CLIENT_ID
                    "zVbG8sai3CUM5uiWnewt5GLPFpwE4bUe" // Replace with actual CLIENT_SECRET
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