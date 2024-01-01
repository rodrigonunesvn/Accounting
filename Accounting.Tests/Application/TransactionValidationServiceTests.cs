using Accounting.Application.Validation;
using Accounting.Core.Enum;
using Accounting.Core.Models;

namespace Accounting.Tests.Application
{
	public class TransactionValidationServiceTests
	{
		[Fact]
		public void IsTransactionValid_EntryTransaction_ShouldReturnTrue()
		{
			// Arrange
			var transactionValidationService = new TransactionValidationService();

			// Act
			var isValid = transactionValidationService.IsTransactionValid(
				new Transaction(100, TransactionTypeEnum.Entry),
				0);

			// Assert
			Assert.True(isValid);
		}

		[Fact]
		public void IsTransactionValid_ExitTransactionWithSufficientBalance_ShouldReturnTrue()
		{
			// Arrange
			var transactionValidationService = new TransactionValidationService();

			// Act
			var isValid = transactionValidationService.IsTransactionValid(
				new Transaction(50, TransactionTypeEnum.Exit),
				100);

			// Assert
			Assert.True(isValid);
		}

		[Fact]
		public void IsTransactionValid_ExitTransactionWithInsufficientBalance_ShouldReturnFalse()
		{
			// Arrange
			var transactionValidationService = new TransactionValidationService();

			// Act
			var isValid = transactionValidationService.IsTransactionValid(
				new Transaction(150, TransactionTypeEnum.Exit),
				100);

			// Assert
			Assert.False(isValid);
		}
	}

}
