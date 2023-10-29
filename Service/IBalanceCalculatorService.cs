using System;
namespace esuperfund.Service
{
    public interface IBalanceCalculatorService
    {
        //calculate closing balance method
        Task CalculateClosingBalances();

    }
}

