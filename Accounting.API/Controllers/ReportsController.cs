using Accounting.Application.Services;
using Accounting.Core.DTO.Response;
using Accounting.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly IReportsService _dailyBalanceReportService;

		public ReportsController(IReportsService dailyBalanceReportService)
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

		[HttpGet("CurrentBalance")]
		public async Task<IActionResult> GetCurrentBalance()
		{
			try
			{
				var result = await _dailyBalanceReportService.GetCurrentBalance();

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error while retrieving the current balance.: {ex.Message}");
			}
		}
	}
}
