using Accounting.Core.DTO.Response;

namespace Accounting.Infrastructure
{
    public interface IDailyBalanceRepository
	{
		Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalancesAsync(DateTime startDate, DateTime endDate);
	}
}
