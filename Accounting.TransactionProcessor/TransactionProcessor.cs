using Accounting.TransactionProcessor.Interfaces;

namespace Accounting.TransactionProcessor
{
	public class TransactionProcessor
	{
		private readonly ISqlServerDataAccess _sqlServerDataAccess;
		private readonly IMongoDbDataAccess _mongoDbDataAccess;

		public TransactionProcessor(ISqlServerDataAccess sqlServerDataAccess, IMongoDbDataAccess mongoDbDataAccess)
		{
			_sqlServerDataAccess = sqlServerDataAccess;
			_mongoDbDataAccess = mongoDbDataAccess;
		}

		public async Task ProcessTransactions()
		{
			var currentDate = DateTime.Now;

			var dailyTotal = _sqlServerDataAccess.GetDailyTotal(currentDate);

			await _mongoDbDataAccess.UpdateDailyBalance(currentDate, dailyTotal);
		}
	}
}
