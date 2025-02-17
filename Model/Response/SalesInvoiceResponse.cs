using System;
using System.Collections.Generic;
using SalesInvoicesScheduler.DTO;

namespace SalesInvoicesScheduler.Model.Response
{
    public class SalesInvoiceResponse
    {
        public long Id { get; set; }  
        public string TransactionNo { get; set; }
        public long PersonId { get; set; }
        public string PersonName { get; set; }
        public long? SelectedPOId { get; set; }
        public long? SelectedPQId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Source { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public string Memo { get; set; }
        public decimal Remaining { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal ShippingPrice { get; set; }
        public string ShippingAddress { get; set; }
        public bool IsShipped { get; set; }
        public string ShipVia { get; set; }
        public string ReferenceNo { get; set; }
        public string TrackingNo { get; set; }
        public string Status { get; set; }
        public decimal DiscountPrice { get; set; }
        public string WitholdingType { get; set; }
        public decimal AmountReceive { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Deposit { get; set; }
        public string CustomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool HasDeliveries { get; set; }
        public bool HasCreditMemos { get; set; }
        public decimal CreditMemoBalance { get; set; }
        public string CurrencyCode { get; set; }
        public bool DisableLink { get; set; }
        public int TransactionStatusId { get; set; }
        public string TransactionStatusName { get; set; }
        public string TransactionStatusNameBahasa { get; set; }
        public string TagsString { get; set; }
        public decimal WitholdingAmount { get; set; }
        public string WitholdingAmountCurrencyFormat { get; set; }
        public decimal DiscountPerLines { get; set; }
        public string DiscountPerLinesCurrencyFormat { get; set; }
        public decimal PaymentReceivedAmount { get; set; }
        public string PaymentReceivedAmountCurrencyFormat { get; set; }
        public string RemainingCurrencyFormat { get; set; }
        public string OriginalAmountCurrencyFormat { get; set; }
        public string ShippingPriceCurrencyFormat { get; set; }
        public string TaxAmountCurrencyFormat { get; set; }
        public string DiscountPriceCurrencyFormat { get; set; }
        public string AmountReceiveCurrencyFormat { get; set; }
        public string SubtotalCurrencyFormat { get; set; }
        public string DepositCurrencyFormat { get; set; }

    }
}
