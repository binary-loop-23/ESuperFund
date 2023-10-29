using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using esuperfund.Models;
using esuperfund.Service;
using Microsoft.AspNetCore.Mvc;


namespace esuperfund.Controllers
{
    public class RawBankTransactionController : BaseApiController
    {
        private readonly IRawBankTransactionService _services;

        public RawBankTransactionController(IRawBankTransactionService services)
        {
            _services = services;
        }

        // Solution for 2. a)
        [HttpGet]
        public async Task<IActionResult> GetAllRawBankTransactionAsync()
        {
            var result = await _services.GetAllRawBankTransaction();
            return result.IsSuccess ? Ok(result.rawTransaction) : NotFound();
        }

        // Solution for 2. c)
        [HttpPost]
        public async Task<IActionResult> CreateRawBankTransactionAsync(RawBankTransaction rawBankTransaction)
        {
            if (rawBankTransaction == null)
            {
                return NoContent();
            }
            var result = await _services.AddRawBankTransaction(rawBankTransaction);
            return result.IsSuccess ? Ok() : BadRequest();
        }

        // Solution for 2. d)
        [HttpDelete("{transactionID}")]
        public async Task<IActionResult> DeleteRawBankTransactionAsync(int transactionID)
        {
            var result = await _services.DeleteRawBankTransaction(transactionID);

            return result.IsSuccess ? Ok(result) : BadRequest();
        }

        // Solution for 2. e)
        [HttpPut("{transactionID}")]
        public async Task<IActionResult> UpdateRawBankTransactionAsync(int transactionID, RawBankTransaction transaction)
        {
            var result = await _services.UpdateRawBankTransaction(transactionID, transaction);
            return result.IsSuccess ? Ok(result.rawTransaction) : BadRequest(result.ErrorMessage);
        }

        // add list of transaction at once into the RawBankTransaction table
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> AddListOfTransactions(List<RawBankTransaction> transactionList)
        {
            if (transactionList != null)
            {
                var result = await _services.AddListOfDataRawBankTransaction(transactionList);
                return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
            }
            return BadRequest();
        }

    }
}

