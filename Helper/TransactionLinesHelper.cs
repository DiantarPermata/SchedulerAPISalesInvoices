using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.DTO;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Helpers
{
    public class TransactionLineHelper
    {
        private readonly ApiHelperService _apiHelperService;
        private readonly TimeZoneInfo _jakartaTimeZone;

        public TransactionLineHelper(ApiHelperService apiHelperService)
        {
            _apiHelperService = apiHelperService;
            try
            {
                _jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                _jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta");
            }
        }

        public async Task<List<TransactionLineResponse>> FetchTransactionLinesAsync()
        {
            Console.WriteLine(">>> [TransactionLineHelper] Mulai fetching transaction lines...");
            try
            {
                var endpoint = "sales_invoices";
                var today = DateTime.UtcNow.ToString("yyyy/MM/dd");
                var queryString = $"start_date={today}&end_date={today}";
                var response = await _apiHelperService.SendRequestAsync(endpoint, queryString);
                if (response == null)
                {
                    Console.WriteLine("❌ [TransactionLineHelper] API response transaction lines kosong.");
                    return new List<TransactionLineResponse>();
                }
                var apiResponse = JsonConvert.DeserializeObject<SalesInvoiceTransactionLinesAPI>(response.ToString());
                if (apiResponse == null || apiResponse.SalesInvoices == null)
                {
                    Console.WriteLine("⚠️ [TransactionLineHelper] API response atau SalesInvoices list null.");
                    return new List<TransactionLineResponse>();
                }
                Console.WriteLine($">>> [TransactionLineHelper] Total SalesInvoices untuk transaction lines: {apiResponse.SalesInvoices.Count}");
                var mapped = MapToTransactionLineResponse(apiResponse.SalesInvoices);
                Console.WriteLine("<<< [TransactionLineHelper] Selesai mapping transaction lines.");
                return mapped;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [TransactionLineHelper] Error: {ex.Message}");
                throw;
            }
        }

        private List<TransactionLineResponse> MapToTransactionLineResponse(List<SalesInvoiceTransactionLinesDTO> invoices)
        {
            var result = new List<TransactionLineResponse>();
            var jakartaNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _jakartaTimeZone);

            foreach (var invoice in invoices)
            {
                if (invoice.TransactionLines != null)
                {
                    foreach (var line in invoice.TransactionLines)
                    {
                        var mappedLine = new TransactionLineResponse
                        {
                            Id = line.Id,
                            SalesInvoiceId = invoice.SalesInvoiceId,
                            TransactionNo = invoice.TransactionNo,
                            ProductId = line.Product?.Id ?? 0,
                            ProductName = line.Product?.Name,
                            ProductCode = line.Product?.ProductCode,
                            Amount = line.Amount,
                            Discount = line.Discount,
                            Rate = line.Rate,
                            UnitId = line.Unit?.Id ?? 0,
                            UnitName = line.Unit?.Name,
                            CustomId = line.CustomId,
                            Description = line.Description,
                            HasReturnLine = line.HasReturnLine,
                            Quantity = line.Quantity,
                            SellAccId = line.SellAccId,
                            BuyAccId = line.BuyAccId,
                            CreatedAt = jakartaNow,
                            CreatedBy = "Manual",
                            LastUpdatedAt = jakartaNow,
                            LastUpdatedBy = null
                        };
                        Console.WriteLine($"> Mapped TransactionLine: ID = {mappedLine.Id}, TransactionNo = {mappedLine.TransactionNo}, Product = {mappedLine.ProductName}");
                        result.Add(mappedLine);
                    }
                }
            }
            return result;
        }
    }
}
