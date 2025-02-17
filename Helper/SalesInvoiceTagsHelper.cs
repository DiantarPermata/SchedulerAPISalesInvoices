using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalesInvoicesScheduler.DTO;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Helpers
{
    public class SalesInvoiceTagHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ApiHelperService _apiHelperService;
        private readonly SalesInvoiceHelper _salesInvoiceHelper;

        public SalesInvoiceTagHelper(IConfiguration configuration, ApiHelperService apiHelperService, SalesInvoiceHelper salesInvoiceHelper)
        {
            _configuration = configuration;
            _apiHelperService = apiHelperService;
            _salesInvoiceHelper = salesInvoiceHelper;
        }

        public async Task<List<SalesInvoiceTagResponse>> FetchSalesInvoiceTagsAsync()
        {
            Console.WriteLine(">>> [SalesInvoiceTagHelper] Mulai fetching sales invoice tags...");
            try
            {
                var endpoint = "sales_invoices"; 
                var today = DateTime.UtcNow.ToString("yyyy/MM/dd");
                var queryString = $"start_date={today}&end_date={today}";

                var response = await _apiHelperService.SendRequestAsync(endpoint, queryString);
                if (response == null)
                {
                    Console.WriteLine("❌ [SalesInvoiceTagHelper] Response API tag kosong!");
                    return new List<SalesInvoiceTagResponse>();
                }

                var apiResponse = JsonConvert.DeserializeObject<SalesInvoiceTagDetailAPI>(response.ToString());
                if (apiResponse == null || apiResponse.SalesInvoiceTags == null)
                {
                    Console.WriteLine("⚠️ [SalesInvoiceTagHelper] API response atau SalesInvoiceTags null!");
                    return new List<SalesInvoiceTagResponse>();
                }

                Console.WriteLine($">>> [SalesInvoiceTagHelper] Total SalesInvoiceTags diterima: {apiResponse.SalesInvoiceTags.Count}");
                var salesInvoiceLookup = await _salesInvoiceHelper.GetSalesInvoiceLookupAsync();
                var mappedTags = MapToSalesInvoiceTagResponse(apiResponse.SalesInvoiceTags, salesInvoiceLookup);
                Console.WriteLine("<<< [SalesInvoiceTagHelper] Selesai fetching dan mapping sales invoice tags.");
                return mappedTags;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [SalesInvoiceTagHelper] Error: {ex.Message}");
                throw;
            }
        }
        private List<SalesInvoiceTagResponse> MapToSalesInvoiceTagResponse(List<SalesInvoiceTagDetailDTO> tagDTOs, Dictionary<string, long> salesInvoiceLookup)
        {
            var result = new List<SalesInvoiceTagResponse>();

            foreach (var dto in tagDTOs)
            {
                long salesInvoiceId = salesInvoiceLookup.ContainsKey(dto.TransactionNo.Trim()) ? salesInvoiceLookup[dto.TransactionNo.Trim()] : 0;
                if (dto.Tags != null)
                {
                    foreach (var tag in dto.Tags)
                    {
                        var mappedTag = new SalesInvoiceTagResponse
                        {
                            SalesInvoiceId = salesInvoiceId,
                            TransactionNo = dto.TransactionNo,
                            TagId = tag.Id,
                            TagName = tag.Name,
                            CreatedAt = dto.CreatedAt,
                            CreatedBy = dto.CreatedBy,
                            LastUpdatedAt = dto.LastUpdatedAt,
                            LastUpdatedBy = dto.LastUpdatedBy
                        };
                        Console.WriteLine($"> Mapped Tag Detail: TransactionNo = {dto.TransactionNo}, TagId = {tag.Id}, TagName = {tag.Name}");
                        result.Add(mappedTag);
                    }
                }
            }
            return result;
        }
    }
}
