using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using esuperfund.Service;
using Microsoft.AspNetCore.Mvc;


namespace esuperfund.Controllers
{
    public class BankTransactionController : BaseApiController
    {
        private readonly IBankTransactionService _services;

        public BankTransactionController(IBankTransactionService services)
        {
            _services = services;
        }

        // Solution for 2. b)
        [HttpGet]
        public async Task<IActionResult> GetAllBankTransactionAsync()
        {
            var result = await _services.GetAllCleanBankTransaction();
            return result.IsSuccess ? Ok(result.bankTransaction) : NotFound();
        }
    }
}

