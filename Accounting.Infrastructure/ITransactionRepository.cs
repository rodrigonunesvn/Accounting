using Accounting.Core.Models;

namespace Accounting.Infrastructure
{
    public interface ITransactionRepository
	{
		Task<bool> AddTransaction(Transaction transaction);
		
		Task<decimal> GetCurrentBalance();
	}
}
