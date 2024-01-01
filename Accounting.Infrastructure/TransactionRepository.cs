using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Dapper;
using Accounting.Infrastructure.Cache;
using Accounting.Core.Models;
using Accounting.Infrastructure.DatabaseContext;

namespace Accounting.Infrastructure
{
	public class TransactionRepository : ITransactionRepository
	{
		private readonly AccountingDbContext _dbContext;
		private readonly ITransactionCacheManager _cacheManager;

		public TransactionRepository(AccountingDbContext dbContext, ITransactionCacheManager cacheManager)
		{
			_dbContext = dbContext;
			_cacheManager = cacheManager;
		}

		public async Task<bool> AddTransaction(Transaction transaction)
		{
			_dbContext.Transactions.Add(transaction);
			var rowsAffected = await _dbContext.SaveChangesAsync();

			_cacheManager.InvalidateCache();

			return rowsAffected == 1;
		}

		public async Task<decimal> GetCurrentBalance()
		{
			return await _cacheManager.GetOrSetCurrentBalance(CalculateAndCacheCurrentBalance);
		}

		private async Task<decimal> CalculateAndCacheCurrentBalance()
		{
			const string sql = @"
				SELECT 
					SUM(CASE WHEN TransactionType = 1 THEN Amount ELSE 0 END) - SUM(CASE WHEN TransactionType = 2 THEN Amount ELSE 0 END)
				FROM Transactions;
			";

			var currentBalance = await _dbContext.Database.GetDbConnection().QueryFirstOrDefaultAsync<decimal>(
				sql
			);

			return currentBalance;
		}
	}
}
