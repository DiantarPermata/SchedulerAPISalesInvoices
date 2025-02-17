using System;

namespace SalesInvoicesScheduler.Model.Response
{
    public class TransactionLineResponse
    {
        public long Id { get; set; }
        public long SalesInvoiceId { get; set; }
        public string TransactionNo { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Rate { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public string CustomId { get; set; }
        public string Description { get; set; }
        public bool HasReturnLine { get; set; }
        public decimal Quantity { get; set; }
        public long SellAccId { get; set; }
        public long BuyAccId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
