using System;
namespace esuperfund.Service
{
    public interface IBalanceCalculatorService
    {

        Task CalculateClosingBalances();

    }
}

