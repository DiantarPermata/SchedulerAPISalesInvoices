using System;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class ProductDetailDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("product_code")]
        public string ProductCode { get; set; }
        
        [JsonProperty("source")]
        public string Source { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("init_date")]
        public DateTime? InitDate { get; set; }
        
        // Asumsikan init_quantity dikirim sebagai string dalam JSON
        [JsonProperty("init_quantity")]
        public string InitQuantity { get; set; }
        
        [JsonProperty("active")]
        public bool Active { get; set; }
        
        [JsonProperty("is_bought")]
        public bool IsBought { get; set; }
        
        [JsonProperty("is_sold")]
        public bool IsSold { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        
        [JsonProperty("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        
        [JsonProperty("is_system")]
        public bool IsSystem { get; set; }
        
        [JsonProperty("custom_id")]
        public string CustomId { get; set; }
        
        [JsonProperty("archive")]
        public bool Archive { get; set; }
        
        [JsonProperty("barcode")]
        public string Barcode { get; set; }
        
        [JsonProperty("use_serial_number")]
        public bool UseSerialNumber { get; set; }
        
        [JsonProperty("track_inventory")]
        public bool TrackInventory { get; set; }
        
        [JsonProperty("is_import")]
        public bool IsImport { get; set; }
        
        [JsonProperty("last_created_inventory")]
        public DateTime? LastCreatedInventory { get; set; }
        
        [JsonProperty("last_created_inventory_format_date")]
        public string LastCreatedInventoryFormatDate { get; set; }
        
        // Ubah ke nullable untuk menampung null
        [JsonProperty("last_updated_inventory")]
        public DateTime? LastUpdatedInventory { get; set; }
        
        [JsonProperty("last_updated_inventory_format_date")]
        public string LastUpdatedInventoryFormatDate { get; set; }
        
        // Jika ada kemungkinan nilai null, gunakan decimal?
        [JsonProperty("buffer_quantity")]
        public decimal? BufferQuantity { get; set; }
        
        [JsonProperty("taxable_buy")]
        public bool TaxableBuy { get; set; }
        
        [JsonProperty("taxable_sell")]
        public bool TaxableSell { get; set; }
        
        [JsonProperty("deletable")]
        public bool Deletable { get; set; }
        
        [JsonProperty("editable")]
        public bool Editable { get; set; }
        
        [JsonProperty("unit")]
        public UnitDTO Unit { get; set; }
        
        [JsonProperty("has_purchase")]
        public bool HasPurchase { get; set; }
        
        [JsonProperty("has_sales")]
        public bool HasSales { get; set; }
        
        [JsonProperty("has_transaction_before_last_close_the_book")]
        public bool HasTransactionBeforeLastCloseTheBook { get; set; }
        
        [JsonProperty("product_categories_string")]
        public string ProductCategoriesString { get; set; }
        
        [JsonProperty("is_bundle")]
        public bool IsBundle { get; set; }
        
        // Ubah ke nullable untuk mengantisipasi nilai null dari API
        [JsonProperty("quantity")]
        public decimal? Quantity { get; set; }
        
        // Ubah juga jika perlu
        [JsonProperty("quantity_available")]
        public decimal? QuantityAvailable { get; set; }
        
        // Asumsikan dikirim sebagai string; konversi dapat dilakukan saat mapping jika diperlukan
        [JsonProperty("buy_price_per_unit")]
        public string BuyPricePerUnit { get; set; }
        
        [JsonProperty("last_buy_price")]
        public decimal LastBuyPrice { get; set; }
        
        [JsonProperty("buy_tax_id")]
        public int? BuyTaxId { get; set; }
        
        [JsonProperty("buy_account")]
        public AccountDTO BuyAccount { get; set; }
        
        [JsonProperty("buy_return_account")]
        public AccountDTO BuyReturnAccount { get; set; }
        
        // Asumsikan dikirim sebagai string
        [JsonProperty("sell_price_per_unit")]
        public string SellPricePerUnit { get; set; }
        
        [JsonProperty("sell_tax_id")]
        public int? SellTaxId { get; set; }
        
        [JsonProperty("sell_account")]
        public AccountDTO SellAccount { get; set; }
        
        [JsonProperty("sell_return_account")]
        public AccountDTO SellReturnAccount { get; set; }
        
        [JsonProperty("average_price")]
        public decimal AveragePrice { get; set; }
        
        [JsonProperty("init_price")]
        public string InitPrice { get; set; }
        
        [JsonProperty("inventory_asset_account")]
        public AccountDTO InventoryAssetAccount { get; set; }
        
        [JsonProperty("total_quantity_in_transaction")]
        public TotalQuantityInTransactionDTO TotalQuantityInTransaction { get; set; }
    }
    
    public class UnitDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    
        [JsonProperty("name")]
        public string Name { get; set; }
    }
    
    public class AccountDTO
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
    
        [JsonProperty("name")]
        public string Name { get; set; }
    
        [JsonProperty("number")]
        public string Number { get; set; }
    }
    
    public class TotalQuantityInTransactionDTO
    {
        [JsonProperty("total_quantity_in_sales")]
        public decimal TotalQuantityInSales { get; set; }
    
        [JsonProperty("total_quantity_in_purchases")]
        public decimal TotalQuantityInPurchases { get; set; }
    }
}
