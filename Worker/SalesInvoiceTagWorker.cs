using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Services;

namespace SalesInvoicesScheduler.Worker
{
    public class SalesInvoiceTagWorker
    {
        private readonly ILogger<SalesInvoiceTagWorker> _logger;
        private readonly SalesInvoiceTagHelper _salesInvoiceTagHelper;
        private readonly SalesInvoiceTagService _salesInvoiceTagService;

        public SalesInvoiceTagWorker(ILogger<SalesInvoiceTagWorker> logger, SalesInvoiceTagHelper salesInvoiceTagHelper, SalesInvoiceTagService salesInvoiceTagService)
        {
            _logger = logger;
            _salesInvoiceTagHelper = salesInvoiceTagHelper;
            _salesInvoiceTagService = salesInvoiceTagService;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation($"SalesInvoiceTagWorker started at: {DateTime.UtcNow}");

                // Ambil data sales invoice tags dari API
                var salesInvoiceTags = await _salesInvoiceTagHelper.FetchSalesInvoiceTagsAsync();
                if (salesInvoiceTags == null || salesInvoiceTags.Count == 0)
                {
                    _logger.LogWarning("No sales invoice tags found to save.");
                    return;
                }

                // Simpan ke database
                await _salesInvoiceTagService.SaveSalesInvoiceTagsToDatabaseAsync(salesInvoiceTags);
                _logger.LogInformation("Sales invoice tags saved to database successfully.");

                // Simpan ke file JSON (jika diperlukan)
                await _salesInvoiceTagService.SaveSalesInvoiceTagsToJsonAsync(salesInvoiceTags);
                _logger.LogInformation("Sales invoice tags saved to JSON file successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in SalesInvoiceTagWorker: {ex.Message}", ex);
                throw;
            }
        }
    }
}
