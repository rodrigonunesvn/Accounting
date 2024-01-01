using Accounting.Core.DTO.Response;
using Accounting.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly IDailyBalanceReportService _dailyBalanceReportService;

		public ReportsController(IDailyBalanceReportService dailyBalanceReportService)
		{
			_dailyBalanceReportService = dailyBalanceReportService;
		}

		[HttpGet("DailyBalance")]
		public async Task<ActionResult<IEnumerable<DailyBalanceReportResponse>>> GetDailyBalanceReport(
			[FromQuery] DateTime startDate,
			[FromQuery] DateTime endDate)
		{
			try
			{
				var report = await _dailyBalanceReportService.GetDailyBalanceReportAsync(startDate, endDate);
				return Ok(report);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error while retrieving the daily balance report.: {ex.Message}");
			}
		}
	}
}
