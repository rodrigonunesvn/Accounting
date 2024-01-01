using Accounting.Core.Enum;
using Accounting.Core.Models;
using Accounting.Infrastructure;
using Accounting.Infrastructure.Cache;
using Accounting.Infrastructure.DatabaseContext;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.Dapper;
using System.Data;

namespace Accounting.Tests.Infrastructure
{
	public class TransactionRepositoryTests
	{
		[Fact]
		public async Task AddTransaction_ValidTransaction_ShouldReturnTrue()
		{
			// Arrange
			var mockConnection = new Mock<IDbConnection>();
			var mockCacheManager = new Mock<ITransactionCacheManager>();

			mockConnection.SetupDapperAsync(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
				.ReturnsAsync(1); // 1 row affected, simulating a successful insert

			var transactionRepository = new TransactionRepository((AccountingDbContext)mockConnection.Object, mockCacheManager.Object);

			// Act
			var isSuccess = await transactionRepository.AddTransaction(new Transaction(100, TransactionTypeEnum.Entry));

			// Assert
			Assert.True(isSuccess);
			mockCacheManager.Verify(cache => cache.InvalidateCache(), Times.Once);
		}

		[Fact]
		public async Task GetCurrentBalance_CalculateAndCacheCurrentBalance_ShouldReturnCorrectBalance()
		{
			// Arrange
			var mockConnection = new Mock<IDbConnection>();
			var mockCacheManager = new Mock<ITransactionCacheManager>();

			mockConnection.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<decimal>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
				.ReturnsAsync(200); // Set the expected balance

			var transactionRepository = new TransactionRepository((AccountingDbContext)mockConnection.Object, mockCacheManager.Object);

			// Act
			var balance = await transactionRepository.GetCurrentBalance();

			// Assert
			Assert.Equal(200, balance); 
		}
	}
}