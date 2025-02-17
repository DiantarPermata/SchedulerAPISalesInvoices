using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SalesInvoicesScheduler.Helpers;
using SalesInvoicesScheduler.Services;

namespace SalesInvoicesScheduler.Worker
{
    public class TransactionLineWorker
    {
        private readonly ILogger<TransactionLineWorker> _logger;
        private readonly TransactionLineHelper _transactionLineHelper;
        private readonly TransactionLineService _transactionLineService;

        public TransactionLineWorker(ILogger<TransactionLineWorker> logger, TransactionLineHelper transactionLineHelper, TransactionLineService transactionLineService)
        {
            _logger = logger;
            _transactionLineHelper = transactionLineHelper;
            _transactionLineService = transactionLineService;
        }

        public async Task RunAsync()
        {
            try
            {
                _logger.LogInformation($"TransactionLineWorker dimulai pada: {DateTime.UtcNow}");

                var transactionLines = await _transactionLineHelper.FetchTransactionLinesAsync();
                if (transactionLines == null || transactionLines.Count == 0)
                {
                    _logger.LogWarning("Tidak ada transaction lines yang ditemukan untuk disimpan.");
                    return;
                }

                await _transactionLineService.SaveTransactionLinesToDatabaseAsync(transactionLines);
                _logger.LogInformation("Transaction lines berhasil disimpan ke database.");

                await _transactionLineService.SaveTransactionLinesToJsonAsync(transactionLines);
                _logger.LogInformation("Transaction lines berhasil disimpan ke file JSON.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error pada TransactionLineWorker: {ex.Message}");
                _logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
