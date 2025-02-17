using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Services;

namespace SalesInvoicesScheduler.Worker
{
    public class SalesInvoiceWorker
    {
        private readonly ILogger<SalesInvoiceWorker> _logger;
        private readonly SalesInvoiceHelper _salesInvoiceHelper;
        private readonly SalesInvoiceService _salesInvoiceService;

        public SalesInvoiceWorker(ILogger<SalesInvoiceWorker> logger, SalesInvoiceHelper salesInvoiceHelper, SalesInvoiceService salesInvoiceService)
        {
            _logger = logger;
            _salesInvoiceHelper = salesInvoiceHelper;
            _salesInvoiceService = salesInvoiceService;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation($"SalesInvoiceWorker started at: {DateTime.UtcNow}");

                // Ambil data Sales Invoice dari API
                var invoices = await _salesInvoiceHelper.FetchSalesInvoicesAsync();
                if (invoices == null || invoices.Count == 0)
                {
                    _logger.LogWarning("No sales invoices found to save.");
                    return;
                }

                // Simpan ke database
                await _salesInvoiceService.SaveSalesInvoicesToDatabaseAsync(invoices);
                _logger.LogInformation("Sales invoices saved to database successfully.");

                // Simpan ke file JSON
                await _salesInvoiceService.SaveSalesInvoicesToJsonAsync(invoices);
                _logger.LogInformation("Sales invoices saved to JSON file successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in SalesInvoiceWorker: {ex.Message}");
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }

    }
}
