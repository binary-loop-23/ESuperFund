using System;
using esuperfund.Models;

namespace esuperfund.Service
{
    public interface IBankTransactionService
    {
        //GetAll BankTransaction
        Task<(bool IsSuccess, IEnumerable<BankTransaction>? bankTransaction, string? ErrorMessage)> GetAllCleanBankTransaction();

    }
}

