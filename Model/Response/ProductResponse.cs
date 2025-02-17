using System;

namespace SalesInvoicesScheduler.Model.Response
{
    public class ProductResponse
    {
        public long Id { get; set; }
    
        public string Name { get; set; }
    
        public string ProductCode { get; set; }
    
        public string Source { get; set; }
    
        public string Description { get; set; }
    
        public DateTime? InitDate { get; set; }
    
        public decimal InitQuantity { get; set; }
    
        public bool Active { get; set; }
    
        public bool IsBought { get; set; }
    
        public bool IsSold { get; set; }
    
        public DateTime CreatedAt { get; set; }
    
        public DateTime? LastUpdatedAt { get; set; } // Ubah menjadi nullable
    
        public DateTime? DeletedAt { get; set; }
    
        public bool IsSystem { get; set; }
    
        public string CustomId { get; set; }
    
        public bool Archive { get; set; }
    
        public string Barcode { get; set; }
    
        public bool UseSerialNumber { get; set; }
    
        public bool TrackInventory { get; set; }
    
        public bool IsImport { get; set; }
    
        public DateTime? LastCreatedInventory { get; set; }
    
        public string LastCreatedInventoryFormatDate { get; set; }
    
        public DateTime? LastUpdatedInventory { get; set; }
    
        public string LastUpdatedInventoryFormatDate { get; set; }
    
        public decimal BufferQuantity { get; set; }
    
        public bool TaxableBuy { get; set; }
    
        public bool TaxableSell { get; set; }
    
        public bool Deletable { get; set; }
    
        public bool Editable { get; set; }
    
        public int? UnitId { get; set; }
    
        public string UnitName { get; set; }
    
        public bool HasPurchase { get; set; }
    
        public bool HasSales { get; set; }
    
        public bool HasTransactionBeforeLastCloseTheBook { get; set; }
    
        public string ProductCategoriesString { get; set; }
    
        public bool IsBundle { get; set; }
    
        public decimal Quantity { get; set; }
    
        public decimal QuantityAvailable { get; set; }
    
        public decimal BuyPricePerUnit { get; set; }
    
        public decimal LastBuyPrice { get; set; }
    
        public int? BuyTaxId { get; set; }
    
        public int? BuyAccountId { get; set; }
    
        public string BuyAccountName { get; set; }
    
        public string BuyAccountNumber { get; set; }
    
        public int? BuyReturnAccountId { get; set; }
    
        public string BuyReturnAccountName { get; set; }
    
        public string BuyReturnAccountNumber { get; set; }
    
        public decimal SellPricePerUnit { get; set; }
    
        public int? SellTaxId { get; set; }
    
        public int? SellAccountId { get; set; }
    
        public string SellAccountName { get; set; }
    
        public string SellAccountNumber { get; set; }
    
        public int? SellReturnAccountId { get; set; }
    
        public string SellReturnAccountName { get; set; }
    
        public string SellReturnAccountNumber { get; set; }
    
        public decimal AveragePrice { get; set; }
    
        public decimal InitPrice { get; set; }
    
        public int? InventoryAssetAccountId { get; set; }
    
        public string InventoryAssetAccountName { get; set; }
    
        public string InventoryAssetAccountNumber { get; set; }
    
        public decimal TotalQuantityInSales { get; set; }
    
        public decimal TotalQuantityInPurchases { get; set; }
    }
}
