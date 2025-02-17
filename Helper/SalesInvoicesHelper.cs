using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalesInvoicesScheduler.DTO;
using SalesInvoicesScheduler.Model;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Helpers
{
    public class SalesInvoiceHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ApiHelperService _apiHelperService;

        public SalesInvoiceHelper(IConfiguration configuration, ApiHelperService apiHelperService)
        {
            _configuration = configuration;
            _apiHelperService = apiHelperService;
        }

        public async Task<List<SalesInvoiceResponse>> FetchSalesInvoicesAsync()
        {
            Console.WriteLine(">>> [SalesInvoiceHelper] Mulai fetching sales invoices...");
            try
            {
                var endpoint = "sales_invoices";
                var today = DateTime.UtcNow.ToString("yyyy/MM/dd");
                var queryString = $"start_date={today}&end_date={today}";

                var response = await _apiHelperService.SendRequestAsync(endpoint, queryString);
                if (response == null)
                {
                    Console.WriteLine("❌ [SalesInvoiceHelper] Response API kosong!");
                    return new List<SalesInvoiceResponse>();
                }
                var apiResponse = JsonConvert.DeserializeObject<SalesInvoiceDetailAPI>(response.ToString());
                if (apiResponse == null || apiResponse.SalesInvoices == null)
                {
                    Console.WriteLine("⚠️ [SalesInvoiceHelper] API response atau SalesInvoices list null!");
                    return new List<SalesInvoiceResponse>();
                }
                Console.WriteLine($">>> [SalesInvoiceHelper] Total SalesInvoices diterima: {apiResponse.SalesInvoices.Count}");
                foreach (var invoice in apiResponse.SalesInvoices)
                Console.WriteLine("<<< [SalesInvoiceHelper] Selesai fetching dan mapping sales invoices.");
                return MapToSalesInvoiceResponse(apiResponse.SalesInvoices);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [SalesInvoiceHelper] Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        private List<SalesInvoiceResponse> MapToSalesInvoiceResponse(List<SalesInvoiceDetailDTO> salesInvoices)
        {
            var result = new List<SalesInvoiceResponse>();

            foreach (var dto in salesInvoices)
            {
                if (dto == null)
                {
                    Console.WriteLine("⚠️ [SalesInvoiceHelper] SalesInvoiceDetailDTO null, dilewati.");
                    continue;
                }
                var SalesInvoicesMapped = new SalesInvoiceResponse
                {
                    Id = dto.Id,
                    TransactionNo = dto.TransactionNo,
                    PersonId = dto.PersonDetail?.Id ?? 0,
                    PersonName = dto.PersonDetail?.DisplayName ?? "Unknown",
                    SelectedPOId = dto.SelectedPOId,
                    SelectedPQId = dto.SelectedPQId,
                    Token = dto.Token,
                    Email = dto.Email,
                    Source = dto.Source,
                    Address = dto.Address,
                    Message = dto.Message,
                    Memo = dto.Memo,
                    Remaining = dto.Remaining,
                    OriginalAmount = dto.OriginalAmount,
                    ShippingPrice = dto.ShippingPrice,
                    ShippingAddress = dto.ShippingAddress,
                    IsShipped = dto.IsShipped,
                    ShipVia = dto.ShipVia,
                    ReferenceNo = dto.ReferenceNo,
                    TrackingNo = dto.TrackingNo,
                    Status = dto.Status,
                    DiscountPrice = dto.DiscountPrice,
                    AmountReceive = dto.AmountReceive,
                    Subtotal = dto.Subtotal,
                    Deposit = dto.Deposit,
                    CreatedAt = dto.CreatedAt,
                    CreatedBy = dto.CreatedBy,
                    LastUpdatedAt = dto.LastUpdatedAt=DateTime.Now,
                    LastUpdatedBy = dto.LastUpdatedBy="Scheduller",
                    DeletedAt = dto.DeletedAt,
                    TransactionDate = dto.TransactionDate,
                    ShippingDate = dto.ShippingDate,
                    DueDate = dto.DueDate,
                    HasDeliveries = dto.HasDeliveries,
                    HasCreditMemos = dto.HasCreditMemos,
                    CreditMemoBalance = dto.CreditMemoBalance,
                    CurrencyCode = dto.CurrencyCode,
                    DisableLink = dto.DisableLink,
                    TransactionStatusId = dto.TransactionStatusJurnal?.Id ?? 0,
                    TransactionStatusName = dto.TransactionStatusJurnal?.Name ?? "Unknown",
                    TransactionStatusNameBahasa = dto.TransactionStatusJurnal?.NameBahasa ?? "Unknown",
                    TagsString = dto.TagsString,
                    WitholdingAmount = dto.WitholdingAmount,
                    WitholdingAmountCurrencyFormat = dto.WitholdingAmountCurrencyFormat,
                    DiscountPerLines = dto.DiscountPerLines,
                    DiscountPerLinesCurrencyFormat = dto.DiscountPerLinesCurrencyFormat,
                    PaymentReceivedAmount = dto.PaymentReceivedAmount,
                    PaymentReceivedAmountCurrencyFormat = dto.PaymentReceivedAmountCurrencyFormat,
                    RemainingCurrencyFormat = dto.RemainingCurrencyFormat,
                    OriginalAmountCurrencyFormat = dto.OriginalAmountCurrencyFormat,
                    ShippingPriceCurrencyFormat = dto.ShippingPriceCurrencyFormat,
                    TaxAmountCurrencyFormat = dto.TaxAmountCurrencyFormat,
                    DiscountPriceCurrencyFormat = dto.DiscountPriceCurrencyFormat,
                    AmountReceiveCurrencyFormat = dto.AmountReceiveCurrencyFormat,
                    SubtotalCurrencyFormat = dto.SubtotalCurrencyFormat,
                    DepositCurrencyFormat = dto.DepositCurrencyFormat
                };
                Console.WriteLine($"> Mapped Tag Detail: SalesInvoicesID = {dto.Id}, TransactionNo = {dto.TransactionNo}, PersonName = {dto.PersonDetail.DisplayName}");
                result.Add(SalesInvoicesMapped);
            }

            return result;
        }

        public async Task<Dictionary<string, long>> GetSalesInvoiceLookupAsync()
        {
            var invoices = await FetchSalesInvoicesAsync();
            return invoices.ToDictionary(si => si.TransactionNo.Trim(), si => si.Id);
        }
    }
}
