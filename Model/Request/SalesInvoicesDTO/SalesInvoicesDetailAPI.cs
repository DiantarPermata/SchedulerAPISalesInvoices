using System.Collections.Generic;
using Newtonsoft.Json;
using SalesInvoicesScheduler.Model;

namespace SalesInvoicesScheduler.DTO
{
    public class SalesInvoiceDetailAPI
    {
        [JsonProperty("sales_invoices")]  
        public List<SalesInvoiceDetailDTO> SalesInvoices { get; set; }
    }
}
