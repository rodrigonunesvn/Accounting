using Accounting.Core.DTO.Response;
using Accounting.Core.Services;
using Accounting.Infrastructure;

namespace Accounting.Application.Services
{
    public class ReportsService : IReportsService
	{
		private readonly IReportsRepository _reportsRepository;
		private readonly ITransactionRepository _transactionRepository;

		public ReportsService(IReportsRepository dailyBalanceRepository, ITransactionRepository transactionRepository)
		{
			_reportsRepository = dailyBalanceRepository;
			_transactionRepository = transactionRepository;
		}

		public async Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalanceReportAsync(DateTime startDate, DateTime endDate)
		{
			var dailyBalances = await _reportsRepository.GetDailyBalancesAsync(startDate, endDate);

			return dailyBalances.Select(db => new DailyBalanceReportResponse
			{
				Date = db.Date,
				Total = db.Total
			});
		}

		public async Task<decimal> GetCurrentBalance()
		{
			decimal currentBalance = await _transactionRepository.GetCurrentBalance();

			return currentBalance;
		}
	}
}
