using Accounting.Application.Services;
using Accounting.Application.Validation;
using Accounting.Core.DTO.Request;
using Accounting.Core.Models;
using Accounting.Infrastructure;
using Microsoft.Azure.ServiceBus;
using Moq;

namespace Accounting.Tests.Application
{
	public class TransactionServiceTests
	{
		[Fact]
		public async Task RegisterEntry_ValidEntryTransaction_ShouldSucceed()
		{
			// Arrange
			var mockTransactionRepository = new Mock<ITransactionRepository>();
			var mockQueueClient = new Mock<IQueueClient>();
			var mockTransactionValidationService = new Mock<ITransactionValidationService>();

			var transactionService = new TransactionService(
				mockTransactionRepository.Object,
				mockQueueClient.Object,
				mockTransactionValidationService.Object);

			// Act
			await transactionService.RegisterEntry(new RegisterTransactionRequest { Amount = 100 });

			// Assert
			mockTransactionRepository.Verify(repo => repo.AddTransaction(It.IsAny<Transaction>()), Times.Once);
			mockQueueClient.Verify(client => client.SendAsync(It.IsAny<Message>()), Times.Once);
		}

		[Fact]
		public async Task RegisterExit_ValidExitTransactionWithSufficientBalance_ShouldSucceed()
		{
			// Arrange
			var mockTransactionRepository = new Mock<ITransactionRepository>();
			var mockQueueClient = new Mock<IQueueClient>();
			var mockTransactionValidationService = new Mock<ITransactionValidationService>();

			var transactionService = new TransactionService(
				mockTransactionRepository.Object,
				mockQueueClient.Object,
				mockTransactionValidationService.Object);

			// Act
			await transactionService.RegisterExit(new RegisterTransactionRequest { Amount = 50 });

			// Assert
			mockTransactionRepository.Verify(repo => repo.AddTransaction(It.IsAny<Transaction>()), Times.Once);
			mockQueueClient.Verify(client => client.SendAsync(It.IsAny<Message>()), Times.Once);
		}

	}

}
