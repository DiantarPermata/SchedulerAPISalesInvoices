using System;

namespace SalesInvoicesScheduler.Model.Response
{
    public class DeliveryOrderResponse
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal QtyIn { get; set; }
        public decimal QtyOut { get; set; }
        public decimal ClosingBalance { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.UtcNow;
    }
}
