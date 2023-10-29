using System;
using esuperfund.Data;
using esuperfund.Models;
using esuperfund.Service;
using Microsoft.EntityFrameworkCore;

namespace esuperfund.Provider
{
    public class BankTransactionProvider : IBankTransactionService
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<BankTransactionProvider> _logger;

        // Dependency Inject the required services 
        public BankTransactionProvider(ApplicationDBContext context, ILogger<BankTransactionProvider> logger)
        {
            _context = context;
            _logger = logger;
        }

        // get all transaction from the BankTransactions table
        public async Task<(bool IsSuccess, IEnumerable<BankTransaction>? bankTransaction, string? ErrorMessage)> GetAllCleanBankTransaction()
        {
            try
            {
                var bankTransactions = await _context.BankTransactions.ToListAsync();
                if (bankTransactions != null && bankTransactions.Any())
                {
                    _logger.LogInformation($"Successfully retreive all transactions from BankTransaction table.");
                    return (true, bankTransactions, null);
                }
                return (false, null, "No transactions found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

