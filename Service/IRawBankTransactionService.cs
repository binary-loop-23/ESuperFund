using System;
using esuperfund.Models;

namespace esuperfund.Service
{
    public interface IRawBankTransactionService
    {
        //GetAll RawBankTransaction
        Task<(bool IsSuccess, IEnumerable<RawBankTransaction>? rawTransaction, string? ErrorMessage)> GetAllRawBankTransaction();

        //Add into RawBankTransaction
        Task<(bool IsSuccess, string? ErrorMessage)> AddRawBankTransaction(RawBankTransaction transaction);

        //Delete RawBankTransaction
        Task<(bool IsSuccess, string? ErrorMessage)> DeleteRawBankTransaction(int transactionID);

        //Update RawBankTransaction
        Task<(bool IsSuccess, RawBankTransaction rawTransaction, string? ErrorMessage)> UpdateRawBankTransaction(int transactionID, RawBankTransaction transaction);

    }
}

