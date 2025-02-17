using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Services
{
    public class ProductService
    {
        private readonly string _connectionString;
        private readonly string _jsonFilePath;

        public ProductService(string connectionString, string jsonFilePath)
        {
            _connectionString = connectionString;
            _jsonFilePath = jsonFilePath;
        }

        public async Task SaveProductsToDatabaseAsync(List<ProductResponse> products)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                // Set context info agar trigger mengenali operasi sebagai "Scheduler"
                using (var contextCmd = new SqlCommand("SET CONTEXT_INFO 0x5363686564756C6572", connection))
                {
                    await contextCmd.ExecuteNonQueryAsync();
                }

                foreach (var product in products)
                {
                    // Jika LastUpdatedAt belum diisi, gunakan waktu saat ini
                    var lastUpdatedAt = product.LastUpdatedAt; // Jika sudah didefinisikan sebagai DateTime non-nullable, nilainya pasti ada.

                    var query = @"
                        IF NOT EXISTS (SELECT 1 FROM Products WHERE Id = @Id)
                        BEGIN
                            INSERT INTO Products (
                                Id, Name, ProductCode, Source, Description, InitDate, InitQuantity, Active, IsBought, IsSold,
                                CreatedAt, CreatedBy, LastUpdatedAt, LastUpdatedBy, DeletedAt, IsSystem, CustomId, Archive,
                                Barcode, UseSerialNumber, TrackInventory, IsImport, LastCreatedInventory, LastCreatedInventoryFormatDate,
                                LastUpdatedInventory, LastUpdatedInventoryFormatDate, BufferQuantity, TaxableBuy, TaxableSell,
                                Deletable, Editable, UnitId, HasPurchase, HasSales, HasTransactionBeforeLastCloseTheBook,
                                ProductCategoriesString, IsBundle, Quantity, QuantityAvailable, BuyPricePerUnit, LastBuyPrice,
                                BuyTaxId, BuyAccountId, BuyAccountName, BuyAccountNumber, BuyReturnAccountId, BuyReturnAccountName,
                                BuyReturnAccountNumber, SellPricePerUnit, SellTaxId, SellAccountId, SellAccountName, SellAccountNumber,
                                SellReturnAccountId, SellReturnAccountName, SellReturnAccountNumber, AveragePrice, InitPrice,
                                InventoryAssetAccountId, InventoryAssetAccountName, InventoryAssetAccountNumber,
                                TotalQuantityInSales, TotalQuantityInPurchases
                            )
                            VALUES (
                                @Id, @Name, @ProductCode, @Source, @Description, @InitDate, @InitQuantity, @Active, @IsBought, @IsSold,
                                @CreatedAt, @CreatedBy, @LastUpdatedAt, @LastUpdatedBy, @DeletedAt, @IsSystem, @CustomId, @Archive,
                                @Barcode, @UseSerialNumber, @TrackInventory, @IsImport, @LastCreatedInventory, @LastCreatedInventoryFormatDate,
                                @LastUpdatedInventory, @LastUpdatedInventoryFormatDate, @BufferQuantity, @TaxableBuy, @TaxableSell,
                                @Deletable, @Editable, @UnitId, @HasPurchase, @HasSales, @HasTransactionBeforeLastCloseTheBook,
                                @ProductCategoriesString, @IsBundle, @Quantity, @QuantityAvailable, @BuyPricePerUnit, @LastBuyPrice,
                                @BuyTaxId, @BuyAccountId, @BuyAccountName, @BuyAccountNumber, @BuyReturnAccountId, @BuyReturnAccountName,
                                @BuyReturnAccountNumber, @SellPricePerUnit, @SellTaxId, @SellAccountId, @SellAccountName, @SellAccountNumber,
                                @SellReturnAccountId, @SellReturnAccountName, @SellReturnAccountNumber, @AveragePrice, @InitPrice,
                                @InventoryAssetAccountId, @InventoryAssetAccountName, @InventoryAssetAccountNumber,
                                @TotalQuantityInSales, @TotalQuantityInPurchases
                            )
                        END
                        ELSE
                        BEGIN
                            UPDATE Products
                            SET 
                                Name = @Name,
                                ProductCode = @ProductCode,
                                Source = @Source,
                                Description = @Description,
                                InitDate = @InitDate,
                                InitQuantity = @InitQuantity,
                                Active = @Active,
                                IsBought = @IsBought,
                                IsSold = @IsSold,
                                LastUpdatedAt = @LastUpdatedAt,
                                LastUpdatedBy = @LastUpdatedBy,
                                DeletedAt = @DeletedAt,
                                IsSystem = @IsSystem,
                                CustomId = @CustomId,
                                Archive = @Archive,
                                Barcode = @Barcode,
                                UseSerialNumber = @UseSerialNumber,
                                TrackInventory = @TrackInventory,
                                IsImport = @IsImport,
                                LastCreatedInventory = @LastCreatedInventory,
                                LastCreatedInventoryFormatDate = @LastCreatedInventoryFormatDate,
                                LastUpdatedInventory = @LastUpdatedInventory,
                                LastUpdatedInventoryFormatDate = @LastUpdatedInventoryFormatDate,
                                BufferQuantity = @BufferQuantity,
                                TaxableBuy = @TaxableBuy,
                                TaxableSell = @TaxableSell,
                                Deletable = @Deletable,
                                Editable = @Editable,
                                UnitId = @UnitId,
                                HasPurchase = @HasPurchase,
                                HasSales = @HasSales,
                                HasTransactionBeforeLastCloseTheBook = @HasTransactionBeforeLastCloseTheBook,
                                ProductCategoriesString = @ProductCategoriesString,
                                IsBundle = @IsBundle,
                                Quantity = @Quantity,
                                QuantityAvailable = @QuantityAvailable,
                                BuyPricePerUnit = @BuyPricePerUnit,
                                LastBuyPrice = @LastBuyPrice,
                                BuyTaxId = @BuyTaxId,
                                BuyAccountId = @BuyAccountId,
                                BuyAccountName = @BuyAccountName,
                                BuyAccountNumber = @BuyAccountNumber,
                                BuyReturnAccountId = @BuyReturnAccountId,
                                BuyReturnAccountName = @BuyReturnAccountName,
                                BuyReturnAccountNumber = @BuyReturnAccountNumber,
                                SellPricePerUnit = @SellPricePerUnit,
                                SellTaxId = @SellTaxId,
                                SellAccountId = @SellAccountId,
                                SellAccountName = @SellAccountName,
                                SellAccountNumber = @SellAccountNumber,
                                SellReturnAccountId = @SellReturnAccountId,
                                SellReturnAccountName = @SellReturnAccountName,
                                SellReturnAccountNumber = @SellReturnAccountNumber,
                                AveragePrice = @AveragePrice,
                                InitPrice = @InitPrice,
                                InventoryAssetAccountId = @InventoryAssetAccountId,
                                InventoryAssetAccountName = @InventoryAssetAccountName,
                                InventoryAssetAccountNumber = @InventoryAssetAccountNumber,
                                TotalQuantityInSales = @TotalQuantityInSales,
                                TotalQuantityInPurchases = @TotalQuantityInPurchases
                            WHERE Id = @Id
                        END";

                    using var command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductCode", product.ProductCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Source", product.Source ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", product.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@InitDate", product.InitDate.HasValue ? (object)product.InitDate.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@InitQuantity", product.InitQuantity);
                    command.Parameters.AddWithValue("@Active", product.Active);
                    command.Parameters.AddWithValue("@IsBought", product.IsBought);
                    command.Parameters.AddWithValue("@IsSold", product.IsSold);
                    command.Parameters.AddWithValue("@CreatedAt", product.CreatedAt);
                    command.Parameters.AddWithValue("@CreatedBy", "Scheduler");
                    command.Parameters.AddWithValue("@LastUpdatedAt", product.LastUpdatedAt);
                    command.Parameters.AddWithValue("@LastUpdatedBy", "Scheduler");
                    command.Parameters.AddWithValue("@DeletedAt", product.DeletedAt.HasValue ? (object)product.DeletedAt.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@IsSystem", product.IsSystem);
                    command.Parameters.AddWithValue("@CustomId", product.CustomId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Archive", product.Archive);
                    command.Parameters.AddWithValue("@Barcode", product.Barcode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@UseSerialNumber", product.UseSerialNumber);
                    command.Parameters.AddWithValue("@TrackInventory", product.TrackInventory);
                    command.Parameters.AddWithValue("@IsImport", product.IsImport);
                    command.Parameters.AddWithValue("@LastCreatedInventory", product.LastCreatedInventory.HasValue ? (object)product.LastCreatedInventory.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@LastCreatedInventoryFormatDate", product.LastCreatedInventoryFormatDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastUpdatedInventory", product.LastUpdatedInventory.HasValue ? (object)product.LastUpdatedInventory.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@LastUpdatedInventoryFormatDate", product.LastUpdatedInventoryFormatDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BufferQuantity", product.BufferQuantity);
                    command.Parameters.AddWithValue("@TaxableBuy", product.TaxableBuy);
                    command.Parameters.AddWithValue("@TaxableSell", product.TaxableSell);
                    command.Parameters.AddWithValue("@Deletable", product.Deletable);
                    command.Parameters.AddWithValue("@Editable", product.Editable);
                    command.Parameters.AddWithValue("@UnitId", product.UnitId.HasValue ? (object)product.UnitId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@HasPurchase", product.HasPurchase);
                    command.Parameters.AddWithValue("@HasSales", product.HasSales);
                    command.Parameters.AddWithValue("@HasTransactionBeforeLastCloseTheBook", product.HasTransactionBeforeLastCloseTheBook);
                    command.Parameters.AddWithValue("@ProductCategoriesString", product.ProductCategoriesString ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsBundle", product.IsBundle);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);
                    command.Parameters.AddWithValue("@QuantityAvailable", product.QuantityAvailable);
                    command.Parameters.AddWithValue("@BuyPricePerUnit", product.BuyPricePerUnit);
                    command.Parameters.AddWithValue("@LastBuyPrice", product.LastBuyPrice);
                    command.Parameters.AddWithValue("@BuyTaxId", product.BuyTaxId.HasValue ? (object)product.BuyTaxId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@BuyAccountId", product.BuyAccountId.HasValue ? (object)product.BuyAccountId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@BuyAccountName", product.BuyAccountName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BuyAccountNumber", product.BuyAccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BuyReturnAccountId", product.BuyReturnAccountId.HasValue ? (object)product.BuyReturnAccountId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@BuyReturnAccountName", product.BuyReturnAccountName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@BuyReturnAccountNumber", product.BuyReturnAccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellPricePerUnit", product.SellPricePerUnit);
                    command.Parameters.AddWithValue("@SellTaxId", product.SellTaxId.HasValue ? (object)product.SellTaxId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SellAccountId", product.SellAccountId.HasValue ? (object)product.SellAccountId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SellAccountName", product.SellAccountName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellAccountNumber", product.SellAccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellReturnAccountId", product.SellReturnAccountId.HasValue ? (object)product.SellReturnAccountId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SellReturnAccountName", product.SellReturnAccountName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SellReturnAccountNumber", product.SellReturnAccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AveragePrice", product.AveragePrice);
                    command.Parameters.AddWithValue("@InitPrice", product.InitPrice);
                    command.Parameters.AddWithValue("@InventoryAssetAccountId", product.InventoryAssetAccountId.HasValue ? (object)product.InventoryAssetAccountId.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@InventoryAssetAccountName", product.InventoryAssetAccountName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@InventoryAssetAccountNumber", product.InventoryAssetAccountNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TotalQuantityInSales", product.TotalQuantityInSales);
                    command.Parameters.AddWithValue("@TotalQuantityInPurchases", product.TotalQuantityInPurchases);

                    await command.ExecuteNonQueryAsync();
                }

                Console.WriteLine("Products successfully saved to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving products to the database: {ex.Message}");
                throw;
            }
        }

        public async Task SaveProductsToJsonAsync(List<ProductResponse> products)
        {
            try
            {
                Console.WriteLine($"Saving {products.Count} products to JSON file.");

                var directory = Path.GetDirectoryName(_jsonFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var jsonData = JsonConvert.SerializeObject(products, Formatting.Indented);
                await File.WriteAllTextAsync(_jsonFilePath, jsonData);

                Console.WriteLine($"Products successfully saved to: {_jsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving products to JSON: {ex.Message}");
                throw;
            }
        }
    }
}
