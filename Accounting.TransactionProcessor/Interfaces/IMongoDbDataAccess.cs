namespace Accounting.TransactionProcessor.Interfaces
{
	public interface IMongoDbDataAccess
	{
		Task UpdateDailyBalance(DateTime currentDate, decimal dailyTotal);
	}
}
