using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class ProductUnitDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unit_code")]
        public string UnitCode { get; set; }

        [JsonProperty("archive")]
        public bool Archive { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class ProductUnitAPIResponse
    {
        [JsonProperty("product_unit")]
        public List<ProductUnitDTO> ProductUnits { get; set; }

        [JsonProperty("default_filter_count")]
        public int DefaultFilterCount { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("total_data")]
        public int TotalData { get; set; }
        
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}