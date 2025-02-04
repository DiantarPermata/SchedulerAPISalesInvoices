using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Helpers.HelperService;

namespace SalesInvoicesScheduler.Helpers.HelperService
{
    public class ApiHelperService
    {
        private readonly HmacHelperService _hmacHelper;

        public ApiHelperService(HmacHelperService hmacHelper)
        {
            _hmacHelper = hmacHelper;
        }

        public async Task<object> SendGetRequestAsync(string url, string clientId, string clientSecret)
        {
            try
            {
                using var client = new HttpClient();
                var dateString = DateTime.UtcNow.ToString("R");
                var requestLine = $"GET {new Uri(url).PathAndQuery} HTTP/1.1";

                var digest = _hmacHelper.ComputeHmacSignature(dateString, requestLine, clientSecret);
                var authorizationHeader = $"hmac username=\"{clientId}\", algorithm=\"hmac-sha256\", headers=\"date request-line\", signature=\"{digest}\"";

                client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
                client.DefaultRequestHeaders.Add("Date", dateString);

                Console.WriteLine("Making API request...");
                var response = await client.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"API returned {response.StatusCode}: {jsonResponse}");
                }

                return JsonConvert.DeserializeObject(jsonResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendGetRequestAsync: {ex.Message}");
                throw;
            }
        }
    }
}