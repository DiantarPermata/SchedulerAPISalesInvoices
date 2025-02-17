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
    public class DeliveryOrderHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ApiHelperService _apiHelperService;

        public DeliveryOrderHelper(IConfiguration configuration, ApiHelperService apiHelperService)
        {
            _configuration = configuration;
            _apiHelperService = apiHelperService;
        }

        public async Task<List<DeliveryOrderResponse>> FetchDeliveryOrdersAsync()
        {
            Console.WriteLine(">>> [DeliveryOrderHelper] Mulai fetching delivery orders...");
            try
            {
                var queryString = "";
                var endpoint = "warehouse_items_stock_movement_summary";

                   var response = await _apiHelperService.SendRequestAsync(endpoint, queryString);
                if (response == null)
                {
                    Console.WriteLine("❌ [DeliveryOrderHelper] API response delivery orders is null!");
                    return new List<DeliveryOrderResponse>();
                }

                var apiResponse = JsonConvert.DeserializeObject<DeliveryOrderAPIDetail>(response.ToString());
                if (apiResponse == null)
                {
                    Console.WriteLine("❌ [DeliveryOrderHelper] Deserialized API response is null!");
                    return new List<DeliveryOrderResponse>();
                }
                if (apiResponse.Summary == null || apiResponse.Summary.Lists == null || !apiResponse.Summary.Lists.Any())
                {
                    Console.WriteLine("⚠️ [DeliveryOrderHelper] API response Summary or Lists is null/empty!");
                    return new List<DeliveryOrderResponse>();
                }

                var productDTOs = apiResponse.Summary.Lists.First().Products;
                if (productDTOs == null)
                {
                    Console.WriteLine("⚠️ [DeliveryOrderHelper] Products in the first list is null!");
                    return new List<DeliveryOrderResponse>();
                }

                Console.WriteLine($">>> [DeliveryOrderHelper] Total Delivery Orders diterima: {productDTOs.Count}");
                var mappedOrders = MapToDeliveryOrderResponse(productDTOs);
                Console.WriteLine("<<< [DeliveryOrderHelper] Finished fetching and mapping delivery orders.");
                return mappedOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [DeliveryOrderHelper] Error: {ex.Message}");
                throw;
            }
        }

        private List<DeliveryOrderResponse> MapToDeliveryOrderResponse(List<DeliveryOrderDetailDTO> productDTOs)
        {
            var result = new List<DeliveryOrderResponse>();

            foreach (var dto in productDTOs)
            {
                var mappedOrder = new DeliveryOrderResponse
                {
                    ProductCode = dto.ProductCode,
                    ProductName = dto.ProductName,
                    OpeningBalance = dto.OpeningBalance,
                    QtyIn = dto.QtyIn,
                    QtyOut = dto.QtyOut,
                    ClosingBalance = dto.ClosingBalance,
                    DeliveryDate = DateTime.UtcNow // Atau lakukan konversi jika perlu
                };

                Console.WriteLine($"> Mapped Delivery Order: ProductCode = {dto.ProductCode}, ProductName = {dto.ProductName}");
                result.Add(mappedOrder);
            }
            return result;
        }
    }
}