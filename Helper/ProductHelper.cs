using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SalesInvoicesScheduler.DTO;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Helpers
{
    public class ProductHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ApiHelperService _apiHelperService;

        public ProductHelper(IConfiguration configuration, ApiHelperService apiHelperService)
        {
            _configuration = configuration;
            _apiHelperService = apiHelperService;
        }

        public async Task<List<ProductResponse>> FetchProductsAsync()
        {
            try
            {
                var endpoint = "products";
                var queryString = "page_size=200";
                Console.WriteLine("Fetching products...");

                var response = await _apiHelperService.SendRequestAsync(endpoint, queryString);
                if (response == null)
                {
                    Console.WriteLine("Warning: Response is null!");
                    return new List<ProductResponse>();
                }

                var apiResponse = JsonConvert.DeserializeObject<ProductDetailAPI>(response.ToString());
                if (apiResponse == null || apiResponse.Products == null)
                {
                    Console.WriteLine("Warning: API response or Products list is null!");
                    return new List<ProductResponse>();
                }

                Console.WriteLine("Products fetched successfully.");
                return MapToProductResponseDTO(apiResponse.Products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FetchProductsAsync: {ex.Message}");
                throw;
            }
        }

        private List<ProductResponse> MapToProductResponseDTO(List<ProductDetailDTO> productDetails)
        {
            var result = new List<ProductResponse>();

            if (productDetails == null)
                return result;

            foreach (var detail in productDetails)
            {
                try
                {
                    var dto = new ProductResponse
                    {
                        Id = detail.Id,
                        Name = detail.Name,
                        ProductCode = detail.ProductCode,
                        Source = detail.Source,
                        Description = detail.Description,
                        InitDate = detail.InitDate ?? DateTime.UtcNow,
                        InitQuantity = Convert.ToDecimal(detail.InitQuantity, CultureInfo.InvariantCulture),
                        Active = detail.Active,
                        IsBought = detail.IsBought,
                        IsSold = detail.IsSold,
                        CreatedAt = detail.CreatedAt,
                        LastUpdatedAt = detail.UpdatedAt, 
                        DeletedAt = detail.DeletedAt,
                        IsSystem = detail.IsSystem,
                        CustomId = detail.CustomId,
                        Archive = detail.Archive,
                        Barcode = detail.Barcode,
                        UseSerialNumber = detail.UseSerialNumber,
                        TrackInventory = detail.TrackInventory,
                        IsImport = detail.IsImport,
                        LastCreatedInventory = detail.LastCreatedInventory,
                        LastCreatedInventoryFormatDate = detail.LastCreatedInventoryFormatDate,
                        LastUpdatedInventory = detail.LastUpdatedInventory,
                        LastUpdatedInventoryFormatDate = detail.LastUpdatedInventoryFormatDate,
                        BufferQuantity = detail.BufferQuantity ?? 0,
                        TaxableBuy = detail.TaxableBuy,
                        TaxableSell = detail.TaxableSell,
                        Deletable = detail.Deletable,
                        Editable = detail.Editable,
                        UnitId = detail.Unit?.Id,
                        UnitName = detail.Unit?.Name,
                        HasPurchase = detail.HasPurchase,
                        HasSales = detail.HasSales,
                        HasTransactionBeforeLastCloseTheBook = detail.HasTransactionBeforeLastCloseTheBook,
                        ProductCategoriesString = detail.ProductCategoriesString,
                        IsBundle = detail.IsBundle,
                        Quantity = detail.Quantity ?? 0,
                        QuantityAvailable = detail.QuantityAvailable ?? 0,
                        BuyPricePerUnit = Convert.ToDecimal(detail.BuyPricePerUnit, CultureInfo.InvariantCulture),
                        LastBuyPrice = detail.LastBuyPrice,
                        BuyTaxId = detail.BuyTaxId,
                        BuyAccountId = detail.BuyAccount?.Id,
                        BuyAccountName = detail.BuyAccount?.Name,
                        BuyAccountNumber = detail.BuyAccount?.Number,
                        BuyReturnAccountId = detail.BuyReturnAccount?.Id,
                        BuyReturnAccountName = detail.BuyReturnAccount?.Name,
                        BuyReturnAccountNumber = detail.BuyReturnAccount?.Number,
                        SellPricePerUnit = Convert.ToDecimal(detail.SellPricePerUnit, CultureInfo.InvariantCulture),
                        SellTaxId = detail.SellTaxId,
                        SellAccountId = detail.SellAccount?.Id,
                        SellAccountName = detail.SellAccount?.Name,
                        SellAccountNumber = detail.SellAccount?.Number,
                        SellReturnAccountId = detail.SellReturnAccount?.Id,
                        SellReturnAccountName = detail.SellReturnAccount?.Name,
                        SellReturnAccountNumber = detail.SellReturnAccount?.Number,
                        AveragePrice = detail.AveragePrice,
                        InitPrice = Convert.ToDecimal(detail.InitPrice, CultureInfo.InvariantCulture),
                        InventoryAssetAccountId = detail.InventoryAssetAccount?.Id,
                        InventoryAssetAccountName = detail.InventoryAssetAccount?.Name,
                        InventoryAssetAccountNumber = detail.InventoryAssetAccount?.Number,
                        TotalQuantityInSales = detail.TotalQuantityInTransaction?.TotalQuantityInSales ?? 0,
                        TotalQuantityInPurchases = detail.TotalQuantityInTransaction?.TotalQuantityInPurchases ?? 0
                    };

                     Console.WriteLine($"Mapped Product: ID={dto.Id}, Name={dto.Name}");
                    result.Add(dto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error mapping product with ID {detail.Id}: {ex.Message}");
                }
            }

            return result;
        }  
    }
}
