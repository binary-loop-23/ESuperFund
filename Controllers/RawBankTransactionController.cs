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


        [HttpGet]
        public async Task<IActionResult> GetAllRawBankTransactionAsync()
        {
            var result = await _services.GetAllRawBankTransaction();
            return result.IsSuccess ? Ok(result.rawTransaction) : NotFound();
        }

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

        [HttpDelete("{transactionID}")]
        public async Task<IActionResult> DeleteRawBankTransactionAsync(int transactionID)
        {
            var result = await _services.DeleteRawBankTransaction(transactionID);

            return result.IsSuccess ? Ok(result) : BadRequest();
        }

        [HttpPut("{transactionID}")]
        public async Task<IActionResult> UpdateDepartmentAsync(int transactionID, RawBankTransaction transaction)
        {
            var result = await _services.UpdateRawBankTransaction(transactionID, transaction);
            return result.IsSuccess ? Ok(result.rawTransaction) : BadRequest(result.ErrorMessage);
        }

    }
}

