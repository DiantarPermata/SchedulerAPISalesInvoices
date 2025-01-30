using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SalesInvoicesScheduler.DTO
{
    public class TransactionStatusDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_bahasa")]
        public string NameBahasa { get; set; }
    }

    public class LineTaxDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("children")]
        public List<object> Children { get; set; }
    }

    public class ProductDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("archive")]
        public bool Archive { get; set; }

        [JsonProperty("track_inventory")]
        public bool TrackInventory { get; set; }

        [JsonProperty("sell_price_per_unit")]
        public string SellPricePerUnit { get; set; }

        [JsonProperty("buy_price_per_unit")]
        public string BuyPricePerUnit { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("product_custom_id")]
        public string ProductCustomId { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("quantity_string")]
        public string QuantityString { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("use_serial_number")]
        public bool UseSerialNumber { get; set; }

        [JsonProperty("has_batch")]
        public bool HasBatch { get; set; }

        [JsonProperty("bundle_non_track")]
        public bool BundleNonTrack { get; set; }
    }

    public class UnitDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TransactionLineAttributeDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("discount")]
        public string Discount { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("tax")]
        public string Tax { get; set; }

        [JsonProperty("line_tax")]
        public LineTaxDTO LineTax { get; set; }

        [JsonProperty("amount_currency_format")]
        public string AmountCurrencyFormat { get; set; }

        [JsonProperty("rate_currency_format")]
        public string RateCurrencyFormat { get; set; }

        [JsonProperty("has_return_line")]
        public bool HasReturnLine { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

        [JsonProperty("sell_acc_id")]
        public int SellAccId { get; set; }

        [JsonProperty("buy_acc_id")]
        public int BuyAccId { get; set; }

        [JsonProperty("product")]
        public ProductDTO Product { get; set; }

        [JsonProperty("unit")]
        public UnitDTO Unit { get; set; }

        [JsonProperty("units")]
        public List<UnitDTO> Units { get; set; }

        [JsonProperty("pricing_rule")]
        public List<object> PricingRule { get; set; }
    }

    public class SalesInvoiceDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("transaction_no")]
        public string TransactionNo { get; set; }

        [JsonProperty("selected_po_id")]
        public int? SelectedPoId { get; set; }

        [JsonProperty("selected_pq_id")]
        public int? SelectedPqId { get; set; }

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
        public string Remaining { get; set; }

        [JsonProperty("original_amount")]
        public string OriginalAmount { get; set; }

        [JsonProperty("shipping_price")]
        public string ShippingPrice { get; set; }

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
        public string TaxAmount { get; set; }

        [JsonProperty("witholding_value")]
        public string WitholdingValue { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("discount_price")]
        public string DiscountPrice { get; set; }

        [JsonProperty("witholding_type")]
        public string WitholdingType { get; set; }

        [JsonProperty("amount_receive")]
        public string AmountReceive { get; set; }

        [JsonProperty("subtotal")]
        public string Subtotal { get; set; }

        [JsonProperty("deposit")]
        public string Deposit { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("transaction_date")]
        public string TransactionDate { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("witholding_amount")]
        public double WitholdingAmount { get; set; }

        [JsonProperty("witholding_amount_currency_format")]
        public string WitholdingAmountCurrencyFormat { get; set; }

        [JsonProperty("discount_per_lines")]
        public string DiscountPerLines { get; set; }

        [JsonProperty("discount_per_lines_currency_format")]
        public string DiscountPerLinesCurrencyFormat { get; set; }

        [JsonProperty("transaction_status")]
        public TransactionStatusDTO TransactionStatus { get; set; }

        [JsonProperty("transaction_lines_attributes")]
        public List<TransactionLineAttributeDTO> TransactionLinesAttributes { get; set; }

        [JsonProperty("payments")]
        public List<object> Payments { get; set; }

        [JsonProperty("sales_returns")]
        public List<object> SalesReturns { get; set; }

        [JsonProperty("sales_deliveries")]
        public List<object> SalesDeliveries { get; set; }

        [JsonProperty("credit_memos")]
        public List<object> CreditMemos { get; set; }

        [JsonProperty("tags")]
        public List<TagDTO> Tags { get; set; }

        [JsonProperty("tags_string")]
        public string TagsString { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }

    public class TagDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SalesInvoicesResponseDTO
    {
        [JsonProperty("sales_invoices")]
        public List<SalesInvoiceDTO> SalesInvoices { get; set; }

        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}

public class CustomDateTimeConverter : JsonConverter<DateTime?>
{
    private readonly string[] _formats = { "dd/MM/yyyy", "yyyy-MM-dd" };

    public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
    {
        writer.WriteValue(value?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
    }

    public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        var dateString = reader.Value.ToString();
        if (DateTime.TryParseExact(dateString, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            return date;
        }

        throw new JsonSerializationException($"Invalid date format: {dateString}");
    }
}
