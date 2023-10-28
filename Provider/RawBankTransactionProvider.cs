using System;
using esuperfund.Data;
using esuperfund.Models;
using esuperfund.Service;
using Microsoft.EntityFrameworkCore;

namespace esuperfund.Provider
{
    public class RawBankTransactionProvider : IRawBankTransactionService
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<RawBankTransactionProvider> _logger;

        public RawBankTransactionProvider(ApplicationDBContext context, ILogger<RawBankTransactionProvider> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddRawBankTransaction(RawBankTransaction transaction)
        {
            try
            {
                using (var dbfeed = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.RawBankTransactions.Add(transaction);
                        await _context.SaveChangesAsync();
                        await dbfeed.CommitAsync();
                        _logger.LogInformation($"New entry into RawBankTransaction table was successfull");
                        return (true, null);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.ToString());
                        return (false, ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteRawBankTransaction(int transactionID)
        {
            try
            {
                var existTransaction = await _context.RawBankTransactions.FirstOrDefaultAsync(e => e.TransactionId == transactionID);
                if (existTransaction == null)
                {
                    return (false, $"Transaction not found");
                }
                _context.RawBankTransactions.Remove(existTransaction);
                _logger.LogInformation($"Successfully deleted an entry from RawBankTransaction");
                await _context.SaveChangesAsync();
                return (true, null);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<RawBankTransaction>? rawTransaction, string? ErrorMessage)> GetAllRawBankTransaction()
        {
            try
            {
                var rawTransactions = await _context.RawBankTransactions.ToListAsync();
                if (rawTransactions != null && rawTransactions.Any())
                {
                    _logger.LogInformation($"Successfully retreive all transactions from RawBankTransaction table.");
                    return (true, rawTransactions, null);
                }
                return (false, null, "No transactions found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, RawBankTransaction? rawTransaction, string? ErrorMessage)> UpdateRawBankTransaction(int transactionID, RawBankTransaction transaction)
        {
            try
            {
                var rawTransactionToUpdate = await _context.RawBankTransactions.FirstOrDefaultAsync(e => e.TransactionId == transactionID);
                if (rawTransactionToUpdate == null)
                {
                    return (false, null, $"Transaction not found");
                }

                using (var dbfeedTransaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        rawTransactionToUpdate.AccountNumber = transaction.AccountNumber;
                        rawTransactionToUpdate.Date = transaction.Date;
                        rawTransactionToUpdate.Narration = transaction.Narration;
                        rawTransactionToUpdate.Amount = rawTransactionToUpdate.Amount;
                        rawTransactionToUpdate.Balance = transaction.Balance;

                        await _context.SaveChangesAsync();
                        await dbfeedTransaction.CommitAsync();
                        _logger.LogInformation($"Successfully updated Raw Transaction table ");
                        return (true, rawTransactionToUpdate, null);
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex.ToString());
                        return (false, null, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }
}

