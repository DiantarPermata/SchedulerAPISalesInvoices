using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class WarehouseItemsStockMovementSummary
    {
        [JsonProperty("list")]
        public List<WarehouseListDTO> Lists { get; set; }
    }

    public class WarehouseListDTO
    {
        [JsonProperty("products")]
        public List<DeliveryOrderDetailDTO> Products { get; set; }
    }

    public class DeliveryOrderDetailDTO
    {
        [JsonProperty("product_code")]
        public string ProductCode { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("opening_balance")]
        public decimal OpeningBalance { get; set; }

        [JsonProperty("qty_in")]
        public decimal QtyIn { get; set; }

        [JsonProperty("qty_out")]
        public decimal QtyOut { get; set; }

        [JsonProperty("closing_balance")]
        public decimal ClosingBalance { get; set; }
    }
}
