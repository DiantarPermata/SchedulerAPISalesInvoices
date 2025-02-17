using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Services
{
    public class TransactionLineService
    {
        private readonly string _connectionString;
        private readonly string _jsonFilePath;
        private readonly TimeZoneInfo _jakartaTimeZone;

        public TransactionLineService(string connectionString, string jsonFilePath)
        {
            _connectionString = connectionString;
            _jsonFilePath = jsonFilePath;
            try
            {
                _jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                _jakartaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Jakarta");
            }
        }

        public async Task SaveTransactionLinesToDatabaseAsync(List<TransactionLineResponse> transactionLines)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using (var contextCmd = new SqlCommand("SET CONTEXT_INFO 0x5363686564756C6572", connection))
                {
                    await contextCmd.ExecuteNonQueryAsync();
                }

                var currentJakartaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _jakartaTimeZone);

                foreach (var line in transactionLines)
                {
                    var query = @"
IF NOT EXISTS (SELECT 1 FROM dbo.TransactionLines WHERE Id = @Id)
BEGIN
    INSERT INTO dbo.TransactionLines (
        Id, SalesInvoiceID, TransactionNo, ProductId, ProductName, ProductCode, Amount, Discount, Rate, UnitId, UnitName, CustomId, Description, HasReturnLine, Quantity, SellAccId, BuyAccId, CreatedAt, CreatedBy, LastUpdatedAt, LastUpdatedBy
    )
    VALUES (
        @Id, @SalesInvoiceID, @TransactionNo, @ProductId, @ProductName, @ProductCode, @Amount, @Discount, @Rate, @UnitId, @UnitName, @CustomId, @Description, @HasReturnLine, @Quantity, @SellAccId, @BuyAccId, @CreatedAt, @CreatedBy, @LastUpdatedAt, @LastUpdatedBy
    )
END
ELSE
BEGIN
    UPDATE dbo.TransactionLines
    SET 
        SalesInvoiceID = @SalesInvoiceID,
        TransactionNo = @TransactionNo,
        ProductId = @ProductId,
        ProductName = @ProductName,
        ProductCode = @ProductCode,
        Amount = @Amount,
        Discount = @Discount,
        Rate = @Rate,
        UnitId = @UnitId,
        UnitName = @UnitName,
        CustomId = @CustomId,
        Description = @Description,
        HasReturnLine = @HasReturnLine,
        Quantity = @Quantity,
        SellAccId = @SellAccId,
        BuyAccId = @BuyAccId,
        LastUpdatedAt = @LastUpdatedAt,
        LastUpdatedBy = @LastUpdatedBy
    WHERE Id = @Id
END";

                    using var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", line.Id);
                    command.Parameters.AddWithValue("@SalesInvoiceID", line.SalesInvoiceId);
                    command.Parameters.AddWithValue("@TransactionNo", line.TransactionNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductId", line.ProductId);
                    command.Parameters.AddWithValue("@ProductName", line.ProductName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ProductCode", line.ProductCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Amount", line.Amount);
                    command.Parameters.AddWithValue("@Discount", line.Discount);
                    command.Parameters.AddWithValue("@Rate", line.Rate);
                    command.Parameters.AddWithValue("@UnitId", line.UnitId);
                    command.Parameters.AddWithValue("@UnitName", line.UnitName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@CustomId", line.CustomId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Description", line.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@HasReturnLine", line.HasReturnLine);
                    command.Parameters.AddWithValue("@Quantity", line.Quantity);
                    command.Parameters.AddWithValue("@SellAccId", line.SellAccId);
                    command.Parameters.AddWithValue("@BuyAccId", line.BuyAccId);
                    command.Parameters.AddWithValue("@CreatedAt", line.CreatedAt);
                    command.Parameters.AddWithValue("@CreatedBy", "Scheduler");
                    command.Parameters.AddWithValue("@LastUpdatedAt", currentJakartaTime);
                    command.Parameters.AddWithValue("@LastUpdatedBy", "Scheduler");

                    await command.ExecuteNonQueryAsync();
                }

                Console.WriteLine("Transaction lines berhasil disimpan ke database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat menyimpan transaction lines ke database: {ex.Message}");
                throw;
            }
        }

        public async Task SaveTransactionLinesToJsonAsync(List<TransactionLineResponse> transactionLines)
        {
            try
            {
                Console.WriteLine($"Menyimpan {transactionLines.Count} transaction lines ke file JSON.");

                var directory = Path.GetDirectoryName(_jsonFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var jsonData = JsonConvert.SerializeObject(transactionLines, Formatting.Indented);
                await File.WriteAllTextAsync(_jsonFilePath, jsonData);

                Console.WriteLine($"Transaction lines berhasil disimpan ke: {_jsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saat menyimpan transaction lines ke JSON: {ex.Message}");
                throw;
            }
        }
    }
}
