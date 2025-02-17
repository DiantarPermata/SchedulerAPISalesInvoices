using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Worker;

namespace SalesInvoicesScheduler
{
    public class SalesInvoicesFunction
    {
        private readonly DeliveryOrderWorker _deliveryOrderWorker;
        private readonly SalesInvoiceWorker _salesInvoiceWorker;
        private readonly ProductWorker _productWorker;
        private readonly SalesInvoiceTagWorker _salesInvoiceTagWorker;
        private readonly TransactionLineWorker _transactionLineWorker;

        public SalesInvoicesFunction(
            DeliveryOrderWorker deliveryOrderWorker,
            ProductWorker productWorker,
            SalesInvoiceWorker salesInvoiceWorker,
            SalesInvoiceTagWorker salesInvoiceTagWorker,
            TransactionLineWorker transactionLineWorker
           )
        {
            _deliveryOrderWorker = deliveryOrderWorker;
            _productWorker = productWorker;
            _salesInvoiceWorker = salesInvoiceWorker;
            _salesInvoiceTagWorker = salesInvoiceTagWorker;
            _transactionLineWorker = transactionLineWorker;
        }

        [FunctionName("SalesInvoicesFunction")]
        public async Task RunAsync([TimerTrigger("%Scheduler:CronSchedule%")] TimerInfo timer, ILogger log)
        {
            log.LogInformation($"SalesInvoicesFunction triggered at: {DateTime.UtcNow}");
            try
            {
                await _deliveryOrderWorker.RunAsync();
                log.LogInformation("Products fetched and logged successfully.");

                await _productWorker.RunAsync();
                log.LogInformation("Products fetched and logged successfully.");

                await _salesInvoiceWorker.RunAsync();
                log.LogInformation("Sales invoices fetched and logged successfully.");

                await _salesInvoiceTagWorker.RunAsync();
                log.LogInformation("Sales invoice tags fetched and logged successfully.");

                await _transactionLineWorker.RunAsync();
                log.LogInformation("Transaction lines fetched and logged successfully.");

            }
            catch (Exception ex)
            {
                log.LogError($"Error occurred: {ex.Message}", ex);
            }
        }
    }
}
