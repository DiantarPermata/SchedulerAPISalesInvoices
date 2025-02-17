using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class SalesInvoiceTagDetailDTO
    {
        [JsonProperty("sales_invoice_id")]
        public long SalesInvoiceId { get; set; }  
        
        [JsonProperty("transaction_no")]
        public string TransactionNo { get; set; }

        [JsonProperty("tags")]
        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; } = "System";

        [JsonProperty("last_updated_at")]
        public DateTime? LastUpdatedAt { get; set; }

        [JsonProperty("last_updated_by")]
        public string LastUpdatedBy { get; set; }
    }

    public class TagDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
