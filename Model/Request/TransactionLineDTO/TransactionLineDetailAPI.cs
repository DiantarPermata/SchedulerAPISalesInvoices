using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class SalesInvoiceTransactionLinesAPI
    {
        [JsonProperty("sales_invoices")]
        public List<SalesInvoiceTransactionLinesDTO> SalesInvoices { get; set; }
    }
}
