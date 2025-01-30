using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;

namespace SalesInvoicesScheduler
{
    public class SalesInvoicesFunction
    {
        private readonly ILogger<SalesInvoicesFunction> _logger;

        public SalesInvoicesFunction(ILogger<SalesInvoicesFunction> logger)
        {
            _logger = logger;
        }

        [FunctionName("SalesInvoicesFunction")]
        public async Task RunAsync([TimerTrigger("0 */1 * * * *")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"SalesInvoicesFunction triggered at: {DateTime.UtcNow}");

            try
            {
                var startDate = DateTime.UtcNow;
                var endDate = DateTime.UtcNow;
                var outputFilePath = "/Users/hyou/Documents/TEST_JSON/Test.json";

                await SalesInvoicesHelper.FetchAndLogSalesInvoicesAsync(
    "ySriK1ZF1hT3x5jU", // Replace with actual CLIENT_ID
    "zVbG8sai3CUM5uiWnewt5GLPFpwE4bUe", // Replace with actual CLIENT_SECRET
    outputFilePath
);


                log.LogInformation("Sales invoices fetched and logged successfully.");
            }
            catch (Exception ex)
            {
                log.LogError($"Error occurred in SalesInvoicesFunction: {ex.Message}", ex);
            }
        }
    }
}
