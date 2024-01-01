using Accounting.Core.DTO.Response;

namespace Accounting.Core.Services
{
    public interface IDailyBalanceReportService
	{
		Task<IEnumerable<DailyBalanceReportResponse>> GetDailyBalanceReportAsync(DateTime startDate, DateTime endDate);
	}
}
