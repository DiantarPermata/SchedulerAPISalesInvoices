using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class ProductDetailAPI
    {
        [JsonProperty("products")]
        public List<ProductDetailDTO> Products { get; set; }
    }
}
