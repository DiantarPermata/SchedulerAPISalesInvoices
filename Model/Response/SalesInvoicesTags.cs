using System;

namespace SalesInvoicesScheduler.Model.Response
{
    public class SalesInvoiceTagResponse
    {
        public long SalesInvoiceId { get; set; }  
        public string TransactionNo { get; set; }
        public int TagId { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
