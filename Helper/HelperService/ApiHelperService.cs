using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.Helpers
{
    public class ApiHelperService
    {
        private readonly IConfiguration _configuration;

        public ApiHelperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<object> SendRequestAsync(string endpoint, string queryString)
        {
            try
            {
                var apiBaseUrl = _configuration["ApiBaseUrl"];
                var clientId = _configuration["ClientId"];
                var clientSecret = _configuration["ClientSecret"];

                var url = $"{apiBaseUrl}{endpoint}?{queryString}";

                using var client = new HttpClient();
                var dateString = DateTime.UtcNow.ToString("R");
                var requestLine = $"GET {new Uri(url).PathAndQuery} HTTP/1.1";

                var digest = HmacHelper.ComputeHmacSignature(dateString, requestLine, clientSecret);
                var authorizationHeader = $"hmac username=\"{clientId}\", algorithm=\"hmac-sha256\", headers=\"date request-line\", signature=\"{digest}\"";

                client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
                client.DefaultRequestHeaders.Add("Date", dateString);

                var response = await client.GetAsync(url);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"API returned {response.StatusCode}: {jsonResponse}");
                }

                return JsonConvert.DeserializeObject(jsonResponse);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in SendRequestAsync: {ex.Message}", ex);
            }
        }
    }
}