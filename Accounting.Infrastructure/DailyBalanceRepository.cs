using Accounting.Core.DTO.Response;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure
{
    public class DailyBalanceRepository : IDailyBalanceRepository
	{
		private readonly IMongoDbContext _mongoDbContext;

		public DailyBalanceRepository(IMongoDbContext mongoDbContext)
		{
			_mongoDbContext = mongoDbContext;
		}

		public async Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalancesAsync(DateTime startDate, DateTime endDate)
		{
			var filter = Builders<DailyBalanceReportResponse>.Filter
				.Gte(db => db.Date, startDate) & Builders<DailyBalanceReportResponse>.Filter
				.Lte(db => db.Date, endDate);

			var sortDefinition = Builders<DailyBalanceReportResponse>.Sort.Ascending(db => db.Date);

			var dailyBalances = await _mongoDbContext.DailyBalances
				.Find(filter)
				.Sort(sortDefinition)
				.ToListAsync();
			
			return dailyBalances.Select(db => new DailyBalanceReportResponse
			{
				Date = db.Date,
				Total = db.Total
			});
		}
	}
}
