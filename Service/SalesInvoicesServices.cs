using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model.Response;

namespace SalesInvoicesScheduler.Services
{
    public class SalesInvoiceService
    {
        private readonly string _connectionString;
        private readonly string _jsonFilePath;

        public SalesInvoiceService(string connectionString, string jsonFilePath)
        {
            _connectionString = connectionString;
            _jsonFilePath = jsonFilePath;
        }

        public async Task SaveSalesInvoicesToDatabaseAsync(List<SalesInvoiceResponse> invoices)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();

                using (var contextCommand = new SqlCommand("SET CONTEXT_INFO 0x5363686564756C6572;", connection))
                {
                    await contextCommand.ExecuteNonQueryAsync();
                }
                foreach (var invoice in invoices)
                {
                    var query = @"
                        IF NOT EXISTS (SELECT 1 FROM SalesInvoices WHERE Id = @Id)
                        BEGIN
                            INSERT INTO SalesInvoices (
                                Id, TransactionNo, PersonId, PersonName, SelectedPOId, SelectedPQId, Token, Email, Source, Address,
                                Message, Memo, Remaining, OriginalAmount, ShippingPrice, ShippingAddress, IsShipped, ShipVia,
                                ReferenceNo, TrackingNo, Status, DiscountPrice, AmountReceive, Subtotal, Deposit, CreatedAt, 
                                CreatedBy, LastUpdatedAt, LastUpdatedBy, DeletedAt, TransactionDate, ShippingDate, DueDate, 
                                HasDeliveries, HasCreditMemos, CreditMemoBalance, CurrencyCode, DisableLink, 
                                TransactionStatusId, TransactionStatusName, TransactionStatusNameBahasa, TagsString, 
                                WitholdingAmount, WitholdingAmountCurrencyFormat, DiscountPerLines, DiscountPerLinesCurrencyFormat, 
                                PaymentReceivedAmount, PaymentReceivedAmountCurrencyFormat, RemainingCurrencyFormat, 
                                OriginalAmountCurrencyFormat, ShippingPriceCurrencyFormat, TaxAmountCurrencyFormat, 
                                DiscountPriceCurrencyFormat, AmountReceiveCurrencyFormat, SubtotalCurrencyFormat, DepositCurrencyFormat
                            )
                            VALUES (
                                @Id, @TransactionNo, @PersonId, @PersonName, @SelectedPOId, @SelectedPQId, @Token, @Email, @Source, @Address,
                                @Message, @Memo, @Remaining, @OriginalAmount, @ShippingPrice, @ShippingAddress, @IsShipped, @ShipVia,
                                @ReferenceNo, @TrackingNo, @Status, @DiscountPrice, @AmountReceive, @Subtotal, @Deposit, @CreatedAt, 
                                @CreatedBy, @LastUpdatedAt, @LastUpdatedBy, @DeletedAt, @TransactionDate, @ShippingDate, @DueDate, 
                                @HasDeliveries, @HasCreditMemos, @CreditMemoBalance, @CurrencyCode, @DisableLink, 
                                @TransactionStatusId, @TransactionStatusName, @TransactionStatusNameBahasa, @TagsString, 
                                @WitholdingAmount, @WitholdingAmountCurrencyFormat, @DiscountPerLines, @DiscountPerLinesCurrencyFormat, 
                                @PaymentReceivedAmount, @PaymentReceivedAmountCurrencyFormat, @RemainingCurrencyFormat, 
                                @OriginalAmountCurrencyFormat, @ShippingPriceCurrencyFormat, @TaxAmountCurrencyFormat, 
                                @DiscountPriceCurrencyFormat, @AmountReceiveCurrencyFormat, @SubtotalCurrencyFormat, @DepositCurrencyFormat
                            )
                        END
                        ELSE
                        BEGIN
                            UPDATE SalesInvoices
                            SET 
                                TransactionNo = @TransactionNo,
                                PersonId = @PersonId,
                                PersonName = @PersonName,
                                SelectedPOId = @SelectedPOId,
                                SelectedPQId = @SelectedPQId,
                                Token = @Token,
                                Email = @Email,
                                Source = @Source,
                                Address = @Address,
                                Message = @Message,
                                Memo = @Memo,
                                Remaining = @Remaining,
                                OriginalAmount = @OriginalAmount,
                                ShippingPrice = @ShippingPrice,
                                ShippingAddress = @ShippingAddress,
                                IsShipped = @IsShipped,
                                ShipVia = @ShipVia,
                                ReferenceNo = @ReferenceNo,
                                TrackingNo = @TrackingNo,
                                Status = @Status,
                                DiscountPrice = @DiscountPrice,
                                AmountReceive = @AmountReceive,
                                Subtotal = @Subtotal,
                                Deposit = @Deposit,
                                LastUpdatedAt = @LastUpdatedAt,
                                LastUpdatedBy = @LastUpdatedBy,
                                DeletedAt = @DeletedAt,
                                TransactionDate = @TransactionDate,
                                ShippingDate = @ShippingDate,
                                DueDate = @DueDate,
                                HasDeliveries = @HasDeliveries,
                                HasCreditMemos = @HasCreditMemos,
                                CreditMemoBalance = @CreditMemoBalance,
                                CurrencyCode = @CurrencyCode,
                                DisableLink = @DisableLink,
                                TransactionStatusId = @TransactionStatusId,
                                TransactionStatusName = @TransactionStatusName,
                                TransactionStatusNameBahasa = @TransactionStatusNameBahasa,
                                TagsString = @TagsString,
                                WitholdingAmount = @WitholdingAmount,
                                WitholdingAmountCurrencyFormat = @WitholdingAmountCurrencyFormat,
                                DiscountPerLines = @DiscountPerLines,
                                DiscountPerLinesCurrencyFormat = @DiscountPerLinesCurrencyFormat,
                                PaymentReceivedAmount = @PaymentReceivedAmount,
                                PaymentReceivedAmountCurrencyFormat = @PaymentReceivedAmountCurrencyFormat,
                                RemainingCurrencyFormat = @RemainingCurrencyFormat,
                                OriginalAmountCurrencyFormat = @OriginalAmountCurrencyFormat,
                                ShippingPriceCurrencyFormat = @ShippingPriceCurrencyFormat,
                                TaxAmountCurrencyFormat = @TaxAmountCurrencyFormat,
                                DiscountPriceCurrencyFormat = @DiscountPriceCurrencyFormat,
                                AmountReceiveCurrencyFormat = @AmountReceiveCurrencyFormat,
                                SubtotalCurrencyFormat = @SubtotalCurrencyFormat,
                                DepositCurrencyFormat = @DepositCurrencyFormat
                            WHERE Id = @Id
                        END";

                    using var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", invoice.Id);
                    command.Parameters.AddWithValue("@TransactionNo", invoice.TransactionNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PersonId", invoice.PersonId);
                    command.Parameters.AddWithValue("@PersonName", invoice.PersonName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SelectedPOId", invoice.SelectedPOId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SelectedPQId", invoice.SelectedPQId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Token", invoice.Token ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", invoice.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Source", invoice.Source ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", invoice.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Message", invoice.Message ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Memo", invoice.Memo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Remaining", invoice.Remaining);
                    command.Parameters.AddWithValue("@OriginalAmount", invoice.OriginalAmount);
                    command.Parameters.AddWithValue("@ShippingPrice", invoice.ShippingPrice);
                    command.Parameters.AddWithValue("@ShippingAddress", invoice.ShippingAddress ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsShipped", invoice.IsShipped);
                    command.Parameters.AddWithValue("@ShipVia", invoice.ShipVia ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ReferenceNo", invoice.ReferenceNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TrackingNo", invoice.TrackingNo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", invoice.Status ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DiscountPrice", invoice.DiscountPrice);
                    command.Parameters.AddWithValue("@AmountReceive", invoice.AmountReceive);
                    command.Parameters.AddWithValue("@Subtotal", invoice.Subtotal);
                    command.Parameters.AddWithValue("@Deposit", invoice.Deposit);
                    command.Parameters.AddWithValue("@CreatedAt", invoice.CreatedAt);
                    command.Parameters.AddWithValue("@CreatedBy", invoice.CreatedBy ?? "Scheduler");
                    command.Parameters.AddWithValue("@LastUpdatedAt", invoice.LastUpdatedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@LastUpdatedBy", invoice.LastUpdatedBy ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DeletedAt", invoice.DeletedAt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TransactionDate", invoice.TransactionDate);
                    command.Parameters.AddWithValue("@ShippingDate", invoice.ShippingDate ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DueDate", invoice.DueDate);
                    command.Parameters.AddWithValue("@HasDeliveries", invoice.HasDeliveries);
                    command.Parameters.AddWithValue("@HasCreditMemos", invoice.HasCreditMemos);
                    command.Parameters.AddWithValue("@CreditMemoBalance", invoice.CreditMemoBalance);
                    command.Parameters.AddWithValue("@CurrencyCode", invoice.CurrencyCode ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DisableLink", invoice.DisableLink);
                    command.Parameters.AddWithValue("@TransactionStatusId", invoice.TransactionStatusId);
                    command.Parameters.AddWithValue("@TransactionStatusName", invoice.TransactionStatusName ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TransactionStatusNameBahasa", invoice.TransactionStatusNameBahasa ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TagsString", invoice.TagsString ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@WitholdingAmount", invoice.WitholdingAmount);
                    command.Parameters.AddWithValue("@WitholdingAmountCurrencyFormat", invoice.WitholdingAmountCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DiscountPerLines", invoice.DiscountPerLines);
                    command.Parameters.AddWithValue("@DiscountPerLinesCurrencyFormat", invoice.DiscountPerLinesCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentReceivedAmount", invoice.PaymentReceivedAmount);
                    command.Parameters.AddWithValue("@PaymentReceivedAmountCurrencyFormat", invoice.PaymentReceivedAmountCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RemainingCurrencyFormat", invoice.RemainingCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@OriginalAmountCurrencyFormat", invoice.OriginalAmountCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ShippingPriceCurrencyFormat", invoice.ShippingPriceCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@TaxAmountCurrencyFormat", invoice.TaxAmountCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DiscountPriceCurrencyFormat", invoice.DiscountPriceCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AmountReceiveCurrencyFormat", invoice.AmountReceiveCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SubtotalCurrencyFormat", invoice.SubtotalCurrencyFormat ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@DepositCurrencyFormat", invoice.DepositCurrencyFormat ?? (object)DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }

                Console.WriteLine("SalesInvoices successfully saved to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving sales invoices to the database: {ex.Message}");
                throw;
            }
        }

        public async Task SaveSalesInvoicesToJsonAsync(List<SalesInvoiceResponse> invoices)
        {
            try
            {
                Console.WriteLine($"Saving {invoices.Count} sales invoices to JSON file.");

                var directory = System.IO.Path.GetDirectoryName(_jsonFilePath);
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

                var jsonData = JsonConvert.SerializeObject(invoices, Formatting.Indented);
                await System.IO.File.WriteAllTextAsync(_jsonFilePath, jsonData);

                Console.WriteLine($"SalesInvoices successfully saved to: {_jsonFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving sales invoices to JSON: {ex.Message}");
                throw;
            }
        }
    }
}
