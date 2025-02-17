using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Services;

namespace SalesInvoicesScheduler.Worker
{
    public class DeliveryOrderWorker
    {
        private readonly ILogger<DeliveryOrderWorker> _logger;
        private readonly DeliveryOrderHelper _deliveryOrderHelper;
        private readonly DeliveryOrderService _deliveryOrderService;

        public DeliveryOrderWorker(ILogger<DeliveryOrderWorker> logger, DeliveryOrderHelper deliveryOrderHelper, DeliveryOrderService deliveryOrderService)
        {
            _logger = logger;
            _deliveryOrderHelper = deliveryOrderHelper;
            _deliveryOrderService = deliveryOrderService;
        }


        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation(">>> [DeliveryOrderWorker] Started fetching and storing delivery orders.");

                var orders = await _deliveryOrderHelper.FetchDeliveryOrdersAsync();
                if (orders == null || orders.Count == 0)
                {
                    _logger.LogWarning("⚠️ [DeliveryOrderWorker] No delivery orders found.");
                    return;
                }

                await _deliveryOrderService.SaveDeliveryOrdersToDatabaseAsync(orders);
                _logger.LogInformation("✅ [DeliveryOrderWorker] Successfully stored delivery orders.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ [DeliveryOrderWorker] Error: {ex.Message}");
                throw;
            }
        }
    }
}
