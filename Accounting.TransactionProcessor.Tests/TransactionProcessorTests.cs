using Accounting.TransactionProcessor.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace Accounting.TransactionProcessor.Tests
{
	public class TransactionProcessorTests
	{
		[Fact]
		public async Task ProcessTransactions_ValidData_ShouldUpdateMongoDb()
		{
			// Arrange
			var currentDate = DateTime.Now;
			var sqlServerDataAccessMock = new Mock<ISqlServerDataAccess>();
			sqlServerDataAccessMock.Setup(x => x.GetDailyTotal(currentDate)).Returns(100);

			var mongoDbDataAccessMock = new Mock<IMongoDbDataAccess>();
			var transactionProcessor = new TransactionProcessor(sqlServerDataAccessMock.Object, mongoDbDataAccessMock.Object);

			// Act
			await transactionProcessor.ProcessTransactions();

			// Assert
			mongoDbDataAccessMock.Verify(x => x.UpdateDailyBalance(currentDate, 100), Times.Once);
		}

		[Fact]
		public async Task ProcessTransactions_DailyTotalZero_ShouldUpdateMongoDbWithZero()
		{
			// Arrange
			var currentDate = DateTime.Now;
			var sqlServerDataAccessMock = new Mock<ISqlServerDataAccess>();
			sqlServerDataAccessMock.Setup(x => x.GetDailyTotal(currentDate)).Returns(0);

			var mongoDbDataAccessMock = new Mock<IMongoDbDataAccess>();
			var transactionProcessor = new TransactionProcessor(sqlServerDataAccessMock.Object, mongoDbDataAccessMock.Object);

			// Act
			await transactionProcessor.ProcessTransactions();

			// Assert
			mongoDbDataAccessMock.Verify(x => x.UpdateDailyBalance(currentDate, 0), Times.Once);
		}

		[Fact]
		public async Task ProcessTransactions_FailureInSqlServer_ShouldLogError()
		{
			// Arrange
			var currentDate = DateTime.Now;
			var sqlServerDataAccessMock = new Mock<ISqlServerDataAccess>();
			sqlServerDataAccessMock.Setup(x => x.GetDailyTotal(currentDate)).Throws(new Exception("Simulated error"));

			var mongoDbDataAccessMock = new Mock<IMongoDbDataAccess>();
			var loggerMock = new Mock<ILogger<TransactionProcessor>>();
			var transactionProcessor = new TransactionProcessor(sqlServerDataAccessMock.Object, mongoDbDataAccessMock.Object);

			// Act
			await transactionProcessor.ProcessTransactions();

			// Assert
			loggerMock.Verify(x => x.LogError(It.IsAny<Exception>(), It.IsAny<string>()), Times.Once);
		}
	}
}
