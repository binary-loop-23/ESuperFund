using System;
using esuperfund.Controllers;
using esuperfund.Models;
using esuperfund.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static System.Collections.Specialized.BitVector32;
using Xunit;
using FluentAssertions;

namespace esuperfund.UnitTesting
{

    public class RawBankTransactionControllerTesting
    {
        private readonly Mock<IRawBankTransactionService> rawBankTransactionServiceStub;
        private readonly RawBankTransactionController controller;

        public RawBankTransactionControllerTesting()
        {
            rawBankTransactionServiceStub = new Mock<IRawBankTransactionService>();
            controller = new RawBankTransactionController(rawBankTransactionServiceStub.Object);
        }


        // Test for GetAllRawBankTransactionAsync when data is not found
        // Should return NotFoundResult
        [Fact]
        public async Task GetRawTransactionListAsync_Returns_NotFound()
        {
            rawBankTransactionServiceStub.Setup(s => s.GetAllRawBankTransaction())
                    .ReturnsAsync((false, null, null));

            var result = await controller.GetAllRawBankTransactionAsync();

            result.Should().BeOfType<NotFoundResult>();
        }


        // Test for GetAllRawBankTransactionAsync when data is found
        // Should return OkObjectResult with a list of transactions
        [Fact]
        public async Task GetRawTransactionListAsync_Returns_Ok()
        {
            var expectedTransactionList = CreateRandomTrasnactionList(5);

            rawBankTransactionServiceStub.Setup(s => s.GetAllRawBankTransaction())
                    .ReturnsAsync((true, expectedTransactionList, null));

            var result = await controller.GetAllRawBankTransactionAsync();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var transaction = okResult.Value.Should().BeAssignableTo<List<RawBankTransaction>>().Subject;

            transaction.Should().BeEquivalentTo(expectedTransactionList, options => options.ComparingByMembers<RawBankTransaction>());
        }

        // Test for CreateRawBankTransactionAsync when adding fails
        // Should return BadRequestResult
        [Fact]
        public async Task CreateRawTransactionAsync_Returns_NotFound()
        {
            var transaction = CreateRandomTransaction();
            rawBankTransactionServiceStub.Setup(s => s.AddRawBankTransaction(transaction))
                .ReturnsAsync((false, null));

            var result = await controller.CreateRawBankTransactionAsync(transaction);

            result.Should().BeOfType<BadRequestResult>();
        }

        // Test for CreateRawBankTransactionAsync when adding succeeds
        // Should return OkResult
        [Fact]
        public async Task CreateRawTransactionAsync_Returns_OkResult()
        {
            var transaction = CreateRandomTransaction();

            rawBankTransactionServiceStub.Setup(s => s.AddRawBankTransaction(transaction))
                .ReturnsAsync((true, null));

            var result = await controller.CreateRawBankTransactionAsync(transaction);

            result.Should().BeOfType<OkResult>();
        }

        // Test for DeleteRawBankTransactionAsync when deletion fails
        // Should return BadRequestResult
        [Fact]
        public async Task DeleteRawTransactionAsync_Returns_NotFound()
        {
            var transactionID = new int();

            rawBankTransactionServiceStub.Setup(s => s.DeleteRawBankTransaction(transactionID))
                .ReturnsAsync((false, null));

            var result = await controller.DeleteRawBankTransactionAsync(transactionID);

            result.Should().BeOfType<BadRequestResult>();
        }

        // Test for DeleteRawBankTransactionAsync when deletion succeeds
        // Should return OkObjectResult
        [Fact]
        public async Task DeleteRawTransactionAsync_Returns_OkResult()
        {
            var transactionID = new int();

            rawBankTransactionServiceStub.Setup(s => s.DeleteRawBankTransaction(transactionID))
                .ReturnsAsync((true, null));

            var result = await controller.DeleteRawBankTransactionAsync(transactionID);

            result.Should().BeOfType<OkObjectResult>();

        }


        // Test for UpdateRawBankTransactionAsync when update succeeds
        // Should return OkObjectResult with the updated transaction
        [Fact]
        public async Task UpdateRawTransactionAsync_Returns_OkResult()
        {
            var transactionId = 1;
            var updatedTransaction = new RawBankTransaction
            {
                AccountNumber = 1000,
                Date = DateTime.Today,
                Narration = "Credit 1",
                Amount = 100,
                Balance = 200
            };
            rawBankTransactionServiceStub.Setup(s => s.UpdateRawBankTransaction(transactionId, updatedTransaction))
                .ReturnsAsync((true, updatedTransaction, null));

            var result = await controller.UpdateRawBankTransactionAsync(transactionId, updatedTransaction);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedTransaction = okResult.Value.Should().BeAssignableTo<RawBankTransaction>().Subject;

            returnedTransaction.Should().BeEquivalentTo(updatedTransaction, options => options.ComparingByMembers<RawBankTransaction>());
        }

        // Test for UpdateRawBankTransactionAsync when update fails
        // Should return BadRequestObjectResult
        [Fact]
        public async Task UpdateRawTransactionAsync_Returns_NotFound()
        {
            var transactionId = 1;
            var updatedTransaction = new RawBankTransaction
            {
                AccountNumber = 1000,
                Date = DateTime.Today,
                Narration = "Credit 1",
                Amount = 100,
                Balance = 200
            };
            rawBankTransactionServiceStub.Setup(s => s.UpdateRawBankTransaction(transactionId, updatedTransaction))
               .ReturnsAsync((false, null, null));

            var result = await controller.UpdateRawBankTransactionAsync(transactionId, updatedTransaction);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // Test for AddListOfTransactions when adding list fails
        // Should return BadRequestObjectResult
        [Fact]
        public async Task CreateRawTransactionListAsync_Returns_NotFound()
        {
            var transactionList = CreateRandomTrasnactionList(5);
            rawBankTransactionServiceStub.Setup(s => s.AddListOfDataRawBankTransaction(transactionList))
                .ReturnsAsync((false, null));

            var result = await controller.AddListOfTransactions(transactionList);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        // Test for AddListOfTransactions when adding list succeeds
        // Should return OkResult
        [Fact]
        public async Task CreateRawTransactionListAsync_Returns_OkResult()
        {
            var transactionList = CreateRandomTrasnactionList(5);

            rawBankTransactionServiceStub.Setup(s => s.AddListOfDataRawBankTransaction(transactionList))
                .ReturnsAsync((true, null));

            var result = await controller.AddListOfTransactions(transactionList);

            result.Should().BeOfType<OkResult>();
        }

        // Create a Sample RawBankTransaction
        public RawBankTransaction CreateRandomTransaction()
        {
            return new()
            {
                AccountNumber = 1000,
                Date = DateTime.Today,
                Narration = "Credit 1",
                Amount = 100,
                Balance = 200
            };
        }

        // Create a list of RawBankTransaction
        public List<RawBankTransaction> CreateRandomTrasnactionList(int count)
        {
            var transactionList = new List<RawBankTransaction>();

            for (int i = 0; i < count; i++)
            {
                transactionList.Add(new RawBankTransaction
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

