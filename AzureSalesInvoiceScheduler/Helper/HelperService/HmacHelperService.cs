using System;
using System.Security.Cryptography;
using System.Text;

namespace SalesInvoicesScheduler.Helpers.HelperService
{
    public class HmacHelperService
    {
        public string ComputeHmacSignature(string date, string requestLine, string clientSecret)
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