using System;
using System.Security.Cryptography;
using System.Text;

namespace SalesInvoicesScheduler.Helpers
{
    public static class HmacHelper
    {
        public static string ComputeHmacSignature(string date, string requestLine, string clientSecret)
        {
            try
            {
                var dataToSign = $"date: {date}\n{requestLine}";
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret));
                var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                return Convert.ToBase64String(signatureBytes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in ComputeHmacSignature: {ex.Message}", ex);
            }
        }
    }
}