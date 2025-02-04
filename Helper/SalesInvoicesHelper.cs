using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Helpers.HelperService;

namespace SalesInvoicesScheduler.Helpers
{
    public class SalesInvoicesHelper
    {
        private readonly string _apiBaseUrl;
        private readonly string _outputFilePath;
        private readonly ApiHelperService _apiHelperService;

        public SalesInvoicesHelper(IConfiguration configuration, ApiHelperService apiHelperService)
        {
            _apiBaseUrl = configuration["ApiBaseUrl"];
            _outputFilePath = configuration["OutputFilePath"];
            _apiHelperService = apiHelperService;
        }

        public async Task FetchAndLogSalesInvoicesAsync(string clientId, string clientSecret)
        {
            try
            {
                var startDate = DateTime.UtcNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var endDate = DateTime.UtcNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine($"Fetching invoices for date range: {startDate} to {endDate}");

                var endpoint = "sales_invoices";
                var url = $"{_apiBaseUrl}{endpoint}?start_date={startDate}&end_date={endDate}";

                var response = await _apiHelperService.SendGetRequestAsync(url, clientId, clientSecret);

                if (response == null)
                {
                    Console.WriteLine("Warning: Response is null!");
                    return;
                }

                var dataWithMetadata = new
                {
                    timestamp = DateTime.Now,
                    dateRange = new { startDate, endDate },
                    data = response
                };

                var jsonContent = JsonConvert.SerializeObject(dataWithMetadata, Formatting.Indented);
                await File.WriteAllTextAsync(_outputFilePath, jsonContent);
                Console.WriteLine($"Sales invoices successfully logged to: {_outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching or logging sales invoices: {ex.Message}");
                throw;
            }
        }
    }
}