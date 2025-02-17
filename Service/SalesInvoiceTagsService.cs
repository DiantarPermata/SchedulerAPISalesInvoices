using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Services
{
    public class SalesInvoiceTagService
    {
        private readonly string _connectionString;
        private readonly string _jsonFilePath;

        public SalesInvoiceTagService(string connectionString, string jsonFilePath)
        {
            _connectionString = connectionString;
            _jsonFilePath = jsonFilePath;
        }

        public async Task SaveSalesInvoiceTagsToDatabaseAsync(List<SalesInvoiceTagResponse> salesInvoiceTags)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using (var contextCmd = new SqlCommand("SET CONTEXT_INFO 0x5363686564756C6572", connection))
                {
                    await contextCmd.ExecuteNonQueryAsync();
                }

                foreach (var tag in salesInvoiceTags)
                {
                    var query = @"
                        IF NOT EXISTS (SELECT 1 FROM SalesInvoiceTags WHERE SalesInvoiceId = @SalesInvoiceId AND TagId = @TagId)
                        BEGIN
                            INSERT INTO SalesInvoiceTags (
                                SalesInvoiceId, TransactionNo, TagId, TagName, CreatedAt, CreatedBy, LastUpdatedAt, LastUpdatedBy
                            )
                            VALUES (
                                @SalesInvoiceId, @TransactionNo, @TagId, @TagName, @CreatedAt, @CreatedBy, @LastUpdatedAt, @LastUpdatedBy
                            )
                        END
                        ELSE
                        BEGIN
                            UPDATE SalesInvoiceTags
                            SET 
                                TransactionNo = @TransactionNo,
                                TagName = @TagName,
                                LastUpdatedAt = @LastUpdatedAt,
                                LastUpdatedBy = @LastUpdatedBy
                            WHERE SalesInvoiceId = @SalesInvoiceId AND TagId = @TagId
                        END";

                    using var command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@SalesInvoiceId", tag.SalesInvoiceId);
                    command.Parameters.AddWithValue("@TransactionNo", tag.TransactionNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TagId", tag.TagId);
                    command.Parameters.AddWithValue("@TagName", tag.TagName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CreatedAt", tag.CreatedAt);
                    command.Parameters.AddWithValue("@CreatedBy", tag.CreatedBy ?? "Scheduler");
                    command.Parameters.AddWithValue("@LastUpdatedAt", tag.LastUpdatedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastUpdatedBy", tag.LastUpdatedBy ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }

                Console.WriteLine("SalesInvoiceTags successfully saved to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving SalesInvoiceTags to the database: {ex.Message}");
                throw;
            }
        }

        public async Task SaveSalesInvoiceTagsToJsonAsync(List<SalesInvoiceTagResponse> salesInvoiceTags)
        {
            try
            {
                Console.WriteLine($"Saving {salesInvoiceTags.Count} SalesInvoiceTags to JSON file.");

                var directory = Path.GetDirectoryName(_jsonFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var jsonData = JsonConvert.SerializeObject(salesInvoiceTags, Formatting.Indented);
                await File.WriteAllTextAsync(_jsonFilePath, jsonData);

                Console.WriteLine($"SalesInvoiceTags successfully saved to: {_jsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving SalesInvoiceTags to JSON: {ex.Message}");
                throw;
            }
        }
    }
}
