using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Services;

namespace SalesInvoicesScheduler.Worker
{
    public class ProductWorker
    {
        private readonly ILogger<ProductWorker> _logger;
        private readonly ProductHelper _productHelper;
        private readonly ProductService _productService;

        public ProductWorker(ILogger<ProductWorker> logger, ProductHelper productHelper, ProductService productService)
        {
            _logger = logger;
            _productHelper = productHelper;
            _productService = productService;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation($"ProductWorker started at: {DateTime.UtcNow}");

                // Ambil data produk dari API
                var products = await _productHelper.FetchProductsAsync();
                if (products == null || products.Count == 0)
                {
                    _logger.LogWarning("No products found to save.");
                    return;
                }

                // Simpan ke database
                await _productService.SaveProductsToDatabaseAsync(products);
                _logger.LogInformation("Products saved to database successfully.");

                // Simpan ke file JSON (jika diperlukan)
                await _productService.SaveProductsToJsonAsync(products);
                _logger.LogInformation("Products saved to JSON file successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred in ProductWorker: {ex.Message}", ex);
                throw;
            }
        }
    }
}
