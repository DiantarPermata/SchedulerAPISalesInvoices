using System;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.Model
{
    public class TransactionStatusDetailDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_bahasa")]
        public string NameBahasa { get; set; }
    }
}
