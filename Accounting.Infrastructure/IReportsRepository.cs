using Accounting.Core.DTO.Response;

namespace Accounting.Infrastructure
{
    public interface IReportsRepository
	{
		Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalancesAsync(DateTime startDate, DateTime endDate);
	}
}
