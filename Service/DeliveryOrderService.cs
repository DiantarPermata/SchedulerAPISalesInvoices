using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Services
{
    public class DeliveryOrderService
    {
        private readonly string _connectionString;
        private readonly string _jsonFilePath;

        public DeliveryOrderService(string connectionString, string jsonFilePath)
        {
            _connectionString = connectionString;
            _jsonFilePath = jsonFilePath;
        }

        public async Task SaveDeliveryOrdersToDatabaseAsync(List<DeliveryOrderResponse> deliveryOrders)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    // Misal target update: hanya update order yang memiliki delivery_date = hari kemarin
                    var targetDate = DateTime.UtcNow.AddDays(-1).Date;

                    foreach (var order in deliveryOrders)
                    {
                        // Jika delivery_date tidak sama dengan targetDate, skip update
                        if (order.DeliveryDate.Date != targetDate)
                        {
                            Console.WriteLine($"⚠️ Skipping order with product_code {order.ProductCode} because its delivery_date ({order.DeliveryDate.Date:d}) is not equal to target date ({targetDate:d}).");
                            continue;
                        }

                        var query = @"
                    UPDATE DeliveryOrder
                    SET product_name = @ProductName, 
                        opening_balance = @OpeningBalance, 
                        qty_in = @QtyIn, 
                        qty_out = @QtyOut, 
                        closing_balance = @ClosingBalance, 
                        updated_at = DATEADD(HOUR, 7, GETUTCDATE())
                    WHERE product_code = @ProductCode 
                      AND delivery_date = @DeliveryDate";

                        using (var command = new SqlCommand(query, connection))
                        {
                            // Pastikan ProductCode tidak null, jika null skip
                            if (string.IsNullOrWhiteSpace(order.ProductCode))
                            {
                                Console.WriteLine("⚠️ Skipping order because ProductCode is null or empty.");
                                continue;
                            }

                            command.Parameters.AddWithValue("@ProductCode", order.ProductCode);
                            command.Parameters.AddWithValue("@ProductName", order.ProductName ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@OpeningBalance", order.OpeningBalance);
                            command.Parameters.AddWithValue("@QtyIn", order.QtyIn);
                            command.Parameters.AddWithValue("@QtyOut", order.QtyOut);
                            command.Parameters.AddWithValue("@ClosingBalance", order.ClosingBalance);
                            command.Parameters.AddWithValue("@DeliveryDate", targetDate);

                            await command.ExecuteNonQueryAsync();
                        }
                    }
                }

                Console.WriteLine("✅ [DeliveryOrderService] Data successfully stored in database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [DeliveryOrderService] Error: {ex.Message}");
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
