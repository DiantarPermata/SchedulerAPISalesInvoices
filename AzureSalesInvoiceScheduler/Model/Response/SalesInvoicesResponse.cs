using System;
using System.Collections.Generic;

namespace SalesInvoicesScheduler.Model.Response
{
    public class TransactionStatusResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameBahasa { get; set; }
    }

    public class LineTaxResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Rate { get; set; }
        public List<object> Children { get; set; }
    }

    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ProductCustomId { get; set; }
        public bool Archive { get; set; }
        public double Quantity { get; set; }
        public string QuantityString { get; set; }
        public bool TrackInventory { get; set; }
        public string SellPricePerUnit { get; set; }
        public string BuyPricePerUnit { get; set; }
        public string Link { get; set; }
    }

    public class UnitResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TransactionLineAttributeResponse
    {
        public int Id { get; set; }
        public string CustomId { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public string Discount { get; set; }
        public string Rate { get; set; }
        public string Tax { get; set; }
        public LineTaxResponse LineTax { get; set; }
        public string AmountCurrencyFormat { get; set; }
        public string RateCurrencyFormat { get; set; }
        public bool HasReturnLine { get; set; }
        public double Quantity { get; set; }
        public int SellAccId { get; set; }
        public int BuyAccId { get; set; }
        public ProductResponse Product { get; set; }
        public UnitResponse Unit { get; set; }
        public List<object> Units { get; set; }
        public List<object> PricingRule { get; set; }
    }

    public class TaxDetailResponse
    {
        public string Name { get; set; }
        public double TaxAmount { get; set; }
        public string TaxAmountCurrencyFormat { get; set; }
    }

    public class SalesInvoiceResponse
    {
        public int Id { get; set; }
        public string TransactionNo { get; set; }
        public int? SelectedPoId { get; set; }
        public int? SelectedPqId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Source { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string Memo { get; set; }
        public string Remaining { get; set; }
        public string OriginalAmount { get; set; }
        public string ShippingPrice { get; set; }
        public bool IsShipped { get; set; }
        public string ShipVia { get; set; }
        public string ReferenceNo { get; set; }
        public string TrackingNo { get; set; }
        public bool TaxAfterDiscount { get; set; }
        public string TaxAmount { get; set; }
        public string WitholdingValue { get; set; }
        public string Status { get; set; }
        public string DiscountPrice { get; set; }
        public string WitholdingType { get; set; }
        public string AmountReceive { get; set; }
        public string Subtotal { get; set; }
        public string Deposit { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? TransactionDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public string CurrencyCode { get; set; }
        public int CurrencyRate { get; set; }
        public List<TransactionLineAttributeResponse> TransactionLinesAttributes { get; set; }
        public List<TaxDetailResponse> TaxDetails { get; set; }
    }

    public class ApiResponse<T>
    {
        public List<T> SalesInvoices { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
