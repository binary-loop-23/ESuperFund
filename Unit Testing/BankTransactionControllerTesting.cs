using System;
using esuperfund.Controllers;
using esuperfund.Models;
using esuperfund.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace esuperfund.UnitTesting
{
    public class BankTransactionControllerTesting
    {
        private readonly Mock<IBankTransactionService> bankTransactionServiceStub;
        private readonly BankTransactionController controller;

        public BankTransactionControllerTesting()
        {
            bankTransactionServiceStub = new Mock<IBankTransactionService>();
            controller = new BankTransactionController(bankTransactionServiceStub.Object);
        }


        // Test for GetAllBankTransactionAsync when data is not found
        // Should return NotFoundResult
        [Fact]
        public async Task GetBankTransactionListAsync_Returns_NotFound()
        {
            bankTransactionServiceStub.Setup(s => s.GetAllCleanBankTransaction())
                    .ReturnsAsync((false, null, null));

            var result = await controller.GetAllBankTransactionAsync();

            result.Should().BeOfType<NotFoundResult>();
        }


        // Test for GetAllBankTransactionAsync when data is found
        // Should return OkObjectResult with a list of transactions
        [Fact]
        public async Task GetBankTransactionListAsync_Returns_Ok()
        {
            var expectedTransactionList = CreateRandomTrasnactionList(5);

            bankTransactionServiceStub.Setup(s => s.GetAllCleanBankTransaction())
                    .ReturnsAsync((true, expectedTransactionList, null));

            var result = await controller.GetAllBankTransactionAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var transaction = okResult.Value.Should().BeAssignableTo<List<BankTransaction>>().Subject;

            transaction.Should().BeEquivalentTo(expectedTransactionList, options => options.ComparingByMembers<BankTransaction>());
        }

        // Create a List of BankTransaction
        public List<BankTransaction> CreateRandomTrasnactionList(int count)
        {
            var transactionList = new List<BankTransaction>();

            for (int i = 0; i < count; i++)
            {
                transactionList.Add(new BankTransaction
                {
                    AccountNumber = 100 + i,
                    Date = DateTime.Today,
                    Narration = $"Credit {i}",
                    Amount = 100,
                    Balance = 200
                });
            }
            return transactionList;
        }

    }
}

