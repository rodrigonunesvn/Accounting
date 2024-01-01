namespace Accounting.TransactionProcessor.Interfaces
{
	public interface ISqlServerDataAccess
	{
		decimal GetDailyTotal(DateTime currentDate);
	}
}
