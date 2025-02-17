using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Globalization;

namespace SalesInvoicesScheduler.Model
{
    public class SalesInvoiceDetailDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }  // Diubah ke long agar cocok dengan API

        [JsonProperty("transaction_no")]
        public string TransactionNo { get; set; }

        [JsonProperty("person")]
        public PersonDTO PersonDetail { get; set; } = new PersonDTO();

        [JsonProperty("selected_po_id")]
        public long? SelectedPOId { get; set; }

        [JsonProperty("selected_pq_id")]
        public long? SelectedPQId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("remaining")]
        public decimal Remaining { get; set; }

        [JsonProperty("original_amount")]
        public decimal OriginalAmount { get; set; }

        [JsonProperty("shipping_price")]
        public decimal ShippingPrice { get; set; }

        [JsonProperty("shipping_address")]
        public string ShippingAddress { get; set; }

        [JsonProperty("is_shipped")]
        public bool IsShipped { get; set; }

        [JsonProperty("ship_via")]
        public string ShipVia { get; set; }

        [JsonProperty("reference_no")]
        public string ReferenceNo { get; set; }

        [JsonProperty("tracking_no")]
        public string TrackingNo { get; set; }

        [JsonProperty("tax_after_discount")]
        public bool TaxAfterDiscount { get; set; }

        [JsonProperty("tax_amount")]
        public decimal TaxAmount { get; set; }

        [JsonProperty("witholding_value")]
        public decimal WitholdingValue { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("discount_price")]
        public decimal DiscountPrice { get; set; }

        [JsonProperty("witholding_type")]
        public string WitholdingType { get; set; }

        [JsonProperty("amount_receive")]
        public decimal AmountReceive { get; set; }

        [JsonProperty("subtotal")]
        public decimal Subtotal { get; set; }

        [JsonProperty("deposit")]
        public decimal Deposit { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; } = "System";

        [JsonProperty("last_updated_at")]
        public DateTime? LastUpdatedAt { get; set; }

        [JsonProperty("last_updated_by")]
        public string LastUpdatedBy { get; set; }

        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("deletable")]
        public bool Deletable { get; set; }

        [JsonProperty("editable")]
        public bool Editable { get; set; }

        [JsonProperty("audited_by")]
        public string AuditedBy { get; set; }

        [JsonProperty("transaction_date")]
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty("shipping_date")]
        public DateTime? ShippingDate { get; set; }

        [JsonProperty("due_date")]
        [JsonConverter(typeof(CustomDateTimeConverter), "dd/MM/yyyy")]
        public DateTime DueDate { get; set; }

        [JsonProperty("witholding_amount")]
        public decimal WitholdingAmount { get; set; }

        [JsonProperty("witholding_amount_currency_format")]
        public string WitholdingAmountCurrencyFormat { get; set; }

        [JsonProperty("discount_per_lines")]
        public decimal DiscountPerLines { get; set; }

        [JsonProperty("discount_per_lines_currency_format")]
        public string DiscountPerLinesCurrencyFormat { get; set; }

        [JsonProperty("payment_received_amount")]
        public decimal PaymentReceivedAmount { get; set; }

        [JsonProperty("payment_received_amount_currency_format")]
        public string PaymentReceivedAmountCurrencyFormat { get; set; }

        [JsonProperty("remaining_currency_format")]
        public string RemainingCurrencyFormat { get; set; }

        [JsonProperty("original_amount_currency_format")]
        public string OriginalAmountCurrencyFormat { get; set; }

        [JsonProperty("shipping_price_currency_format")]
        public string ShippingPriceCurrencyFormat { get; set; }

        [JsonProperty("tax_amount_currency_format")]
        public string TaxAmountCurrencyFormat { get; set; }

        [JsonProperty("discount_price_currency_format")]
        public string DiscountPriceCurrencyFormat { get; set; }

        [JsonProperty("amount_receive_currency_format")]
        public string AmountReceiveCurrencyFormat { get; set; }

        [JsonProperty("subtotal_currency_format")]
        public string SubtotalCurrencyFormat { get; set; }

        [JsonProperty("deposit_currency_format")]
        public string DepositCurrencyFormat { get; set; }

        [JsonProperty("has_deliveries")]
        public bool HasDeliveries { get; set; }

        [JsonProperty("has_credit_memos")]
        public bool HasCreditMemos { get; set; }

        [JsonProperty("credit_memo_balance")]
        public decimal CreditMemoBalance { get; set; }

        [JsonProperty("credit_memo_balance_currency_format")]
        public string CreditMemoBalanceCurrencyFormat { get; set; }

        // Pastikan JsonProperty sesuai dengan API
        [JsonProperty("transaction_status")]
        public TransactionStatusDetailDTO TransactionStatusJurnal { get; set; }

        [JsonProperty("tags_string")]
        public string TagsString { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("disable_link")]
        public bool DisableLink { get; set; }
    }

    public class PersonDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }

    public class CustomDateTimeConverter : JsonConverter<DateTime?>
    {
        private readonly string _dateFormat;
        public CustomDateTimeConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var dateString = reader.Value.ToString();
            if (DateTime.TryParseExact(dateString, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;
            throw new JsonSerializationException($"Unable to parse '{dateString}' to DateTime using format '{_dateFormat}'.");
        }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToString(_dateFormat));
            else
                writer.WriteNull();
        }
    }
}
