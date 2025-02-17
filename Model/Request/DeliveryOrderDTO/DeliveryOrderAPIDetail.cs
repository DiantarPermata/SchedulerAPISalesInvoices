using System.Collections.Generic;
using Newtonsoft.Json;

namespace SalesInvoicesScheduler.DTO
{
    public class DeliveryOrderAPIDetail

    {
        [JsonProperty("warehouse_items_stock_movement_summary")]
        public WarehouseItemsStockMovementSummary Summary { get; set; }
    }
}
