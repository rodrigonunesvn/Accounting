using Accounting.Core.DTO.Response;
using Accounting.Core.Services;
using Accounting.Infrastructure;

namespace Accounting.Application.Services
{
    public class DailyBalanceReportService : IDailyBalanceReportService
	{
		private readonly IDailyBalanceRepository _dailyBalanceRepository;

		public DailyBalanceReportService(IDailyBalanceRepository dailyBalanceRepository)
		{
			_dailyBalanceRepository = dailyBalanceRepository;
		}

		public async Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalanceReportAsync(DateTime startDate, DateTime endDate)
		{
			var dailyBalances = await _dailyBalanceRepository.GetDailyBalancesAsync(startDate, endDate);

			return dailyBalances.Select(db => new DailyBalanceReportResponse
			{
				Date = db.Date,
				Total = db.Total
			});
		}
	}
}
