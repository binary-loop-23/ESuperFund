using System;
using esuperfund.Data;
using esuperfund.Models;
using esuperfund.Service;
using Microsoft.EntityFrameworkCore;

namespace esuperfund.Provider
{
    public class BalanceCalculatorProvider : IBalanceCalculatorService
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<BankTransactionProvider> _logger;

        public BalanceCalculatorProvider(ApplicationDBContext context, ILogger<BankTransactionProvider> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Solution for 2. f)
        public async Task CalculateClosingBalances()
        {

            var groupedTransactions = _context.RawBankTransactions
                .Where(t => t.AccountNumber != null && t.Date != null)
                .OrderBy(t => t.AccountNumber)
                .ThenBy(t => t.Date)
                .GroupBy(t => t.AccountNumber)
                .ToList();

            foreach (var group in groupedTransactions)
            {
                decimal? currentBalance = 0;
                decimal? closingBalance = 0;

                foreach (var transaction in group)
                {
                    currentBalance += transaction.Amount;
                    closingBalance = transaction.Balance;
                }
                if (currentBalance == closingBalance)
                {
                    using (var dbfeedTransaction = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            foreach (var transaction in group)
                            {
                                var existTransaction = await _context.BankTransactions.FirstOrDefaultAsync(t =>
                                t.AccountNumber == transaction.AccountNumber &&
                                t.Date == transaction.Date &&
                                t.Narration == transaction.Narration &&
                                t.Amount == transaction.Amount &&
                                t.Balance == transaction.Balance);

                                if (existTransaction == null)
                                {
                                    var newBankTransaction = new BankTransaction()
                                    {
                                        AccountNumber = (int)transaction.AccountNumber,
                                        Date = (DateTime)transaction.Date,
                                        Narration = transaction.Narration,
                                        Amount = (decimal)transaction.Amount,
                                        Balance = (decimal)transaction.Balance

                                    };
                                    await _context.BankTransactions.AddAsync(newBankTransaction);
                                    await _context.SaveChangesAsync();
                                    await dbfeedTransaction.CommitAsync();
                                }
                                else
                                {
                                    _logger.LogInformation($"Transaction already exists: {transaction.AccountNumber}, {transaction.Date}");
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.ToString());
                        }
                    }

                }
            }
        }
    }
}

