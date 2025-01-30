using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.DTO;

namespace SalesInvoicesScheduler.Helpers
{
    public static class SalesInvoicesHelper
    {
        private const string ApiBaseUrl = "https://api.mekari.com/public/jurnal/api/v1/sales_invoices";

        public static async Task FetchAndLogSalesInvoicesAsync(
            string clientId,
            string clientSecret,
            string outputFilePath)
        {
            try
            {
                // Menggunakan format dd/MM/yyyy
                var startDate = DateTime.UtcNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var endDate = DateTime.UtcNow.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine($"Fetching invoices for date range: {startDate} to {endDate}");

                var response = await FetchSalesInvoicesAsync(clientId, clientSecret, startDate, endDate);

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
                Console.WriteLine($"Serialized content length: {jsonContent.Length}");
                Console.WriteLine($"Content preview: {jsonContent.Substring(0, Math.Min(500, jsonContent.Length))}");

                var directory = Path.GetDirectoryName(outputFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Console.WriteLine($"Created directory: {directory}");
                }

                await File.WriteAllTextAsync(outputFilePath, jsonContent);
                Console.WriteLine($"Sales invoices successfully logged to: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching or logging sales invoices: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public static async Task<SalesInvoicesResponseDTO> FetchSalesInvoicesAsync(
            string clientId,
            string clientSecret,
            string startDate,
            string endDate)
        {
            try
            {
                // Pastikan URL menggunakan format dd/MM/yyyy
                var url = $"{ApiBaseUrl}?start_date={startDate}&end_date={endDate}";
                Console.WriteLine($"Requesting URL: {url}");

                using var client = new HttpClient();

                var dateString = DateTime.UtcNow.ToString("R");
                var requestLine = $"GET {new Uri(url).PathAndQuery} HTTP/1.1";

                Console.WriteLine($"Request Line: {requestLine}");
                Console.WriteLine($"Date String: {dateString}");

                var digest = ComputeHmacSignature(dateString, requestLine, clientSecret);
                var authorizationHeader = $"hmac username=\"{clientId}\", algorithm=\"hmac-sha256\", headers=\"date request-line\", signature=\"{digest}\"";

                client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
                client.DefaultRequestHeaders.Add("Date", dateString);

                Console.WriteLine("Making API request...");
                var response = await client.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"API Response Status: {response.StatusCode}");
                Console.WriteLine($"Raw Response: {jsonResponse}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API returned {response.StatusCode}: {jsonResponse}");
                }

                var deserializedResponse = JsonConvert.DeserializeObject<SalesInvoicesResponseDTO>(jsonResponse);
                Console.WriteLine($"Deserialized response successfully");

                return deserializedResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FetchSalesInvoicesAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private static string ComputeHmacSignature(string date, string requestLine, string clientSecret)
        {
            try
            {
                var dataToSign = $"date: {date}\n{requestLine}";
                Console.WriteLine($"Data to sign: {dataToSign}");

                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret));
                var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                var signature = Convert.ToBase64String(signatureBytes);

                Console.WriteLine($"Generated signature: {signature}");
                return signature;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ComputeHmacSignature: {ex.Message}");
                throw;
            }
        }
    }
}
