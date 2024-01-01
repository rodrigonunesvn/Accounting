using Accounting.API.Controllers;
using Accounting.Core.DTO.Response;
using Accounting.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Accounting.Tests.API
{
	public class ReportsControllerTests
	{
		[Fact]
		public async Task GetDailyBalanceReport_ValidParameters_ShouldReturnOkResult()
		{
			// Arrange
			var startDate = DateTime.Now.AddDays(-7);
			var endDate = DateTime.Now;

			var mockReportService = new Mock<IDailyBalanceReportService>();
			mockReportService.Setup(x => x.GetDailyBalanceReportAsync(startDate, endDate))
							 .ReturnsAsync(new List<DailyBalanceReportResponse>()); 

			var controller = new ReportsController(mockReportService.Object);

			// Act
			var result = await controller.GetDailyBalanceReport(startDate, endDate);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result.Result);
			var model = Assert.IsAssignableFrom<IEnumerable<DailyBalanceReportResponse>>(okResult.Value);
			Assert.NotNull(model);
		}

		[Fact]
		public async Task GetDailyBalanceReport_ServiceThrowsException_ShouldReturnInternalServerError()
		{
			// Arrange
			var startDate = DateTime.Now.AddDays(-7);
			var endDate = DateTime.Now;

			var mockReportService = new Mock<IDailyBalanceReportService>();
			mockReportService.Setup(x => x.GetDailyBalanceReportAsync(startDate, endDate))
							 .ThrowsAsync(new Exception("Some error message"));

			var controller = new ReportsController(mockReportService.Object);

			// Act
			var result = await controller.GetDailyBalanceReport(startDate, endDate);

			// Assert
			var statusCodeResult = Assert.IsType<StatusCodeResult>(result.Result);
			Assert.Equal(500, statusCodeResult.StatusCode);
		}
	}

}
