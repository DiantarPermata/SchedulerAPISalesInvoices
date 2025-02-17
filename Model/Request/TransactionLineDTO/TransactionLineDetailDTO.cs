using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
   public class SalesInvoiceTransactionLinesDTO
    {
        [JsonProperty("id")]
        public long SalesInvoiceId { get; set; }
        
        [JsonProperty("transaction_no")]
        public string TransactionNo { get; set; }

        [JsonProperty("transaction_lines_attributes")]
        public List<TransactionLineDTO> TransactionLines { get; set; }
    }

    public class TransactionLineDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("custom_id")]
        public string CustomId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("discount")]
        public decimal Discount { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("has_return_line")]
        public bool HasReturnLine { get; set; }

        [JsonProperty("quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty("sell_acc_id")]
        public long SellAccId { get; set; }

        [JsonProperty("buy_acc_id")]
        public long BuyAccId { get; set; }

        [JsonProperty("product")]
        public TransactionLineProductDTO Product { get; set; }

        [JsonProperty("unit")]
        public TransactionLineUnitDTO Unit { get; set; }
    }

    public class TransactionLineProductDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("product_code")]
        public string ProductCode { get; set; }
    }

    public class TransactionLineUnitDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}