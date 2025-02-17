using System.Collections.Generic;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model;

namespace SalesInvoicesScheduler.DTO
{
    public class SalesInvoiceTagDetailAPI
    {
        [JsonProperty("sales_invoices")]
        public List<SalesInvoiceTagDetailDTO> SalesInvoiceTags { get; set; }
    }
}
