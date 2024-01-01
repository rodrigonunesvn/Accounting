using Accounting.Core.DTO.Response;

namespace Accounting.Core.Services
{
    public interface IReportsService
	{
		Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalanceReportAsync(DateTime startDate, DateTime endDate);

		Task<decimal> GetCurrentBalance();
	}
}
