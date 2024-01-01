using Accounting.API.Controllers;
using Accounting.Core.DTO.Request;
using Accounting.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Accounting.Tests.API
{
	public class TransactionControllerTests
	{
		[Fact]
		public async Task RegisterEntry_ValidRequest_ShouldReturnOkResult()
		{
			// Arrange
			var mockTransactionService = new Mock<ITransactionService>();
			mockTransactionService.Setup(x => x.RegisterEntry(It.IsAny<RegisterTransactionRequest>()));

			var controller = new TransactionController(mockTransactionService.Object);
			var request = new RegisterTransactionRequest { Amount = 100 };

			// Act
			var result = await controller.RegisterEntry(request);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Entry registered successfully!", okResult.Value);
		}

		[Fact]
		public async Task RegisterExit_ValidRequest_ShouldReturnOkResult()
		{
			// Arrange
			var mockTransactionService = new Mock<ITransactionService>();
			mockTransactionService.Setup(x => x.RegisterExit(It.IsAny<RegisterTransactionRequest>()));

			var controller = new TransactionController(mockTransactionService.Object);
			var request = new RegisterTransactionRequest { Amount = 100 };

			// Act
			var result = await controller.RegisterExit(request);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal("Entry registered successfully!", okResult.Value);
		}

		[Fact]
		public async Task GetCurrentBalance_ValidRequest_ShouldReturnOkResult()
		{
			// Arrange
			var mockTransactionService = new Mock<ITransactionService>();
			mockTransactionService.Setup(x => x.GetCurrentBalance())
								  .ReturnsAsync(500);

			var controller = new TransactionController(mockTransactionService.Object);

			// Act
			var result = await controller.GetCurrentBalance();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			Assert.Equal(500, okResult.Value);
		}

		[Fact]
		public async Task RegisterEntry_ServiceThrowsException_ShouldReturnBadRequest()
		{
			// Arrange
			var mockTransactionService = new Mock<ITransactionService>();
			mockTransactionService.Setup(x => x.RegisterEntry(It.IsAny<RegisterTransactionRequest>()))
								  .ThrowsAsync(new InvalidOperationException("Invalid operation"));

			var controller = new TransactionController(mockTransactionService.Object);
			var request = new RegisterTransactionRequest { Amount = 100 };

			// Act
			var result = await controller.RegisterEntry(request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Invalid operation", badRequestResult.Value);
		}

		[Fact]
		public async Task RegisterExit_InsufficientFunds_ShouldReturnBadRequest()
		{
			// Arrange
			var mockTransactionService = new Mock<ITransactionService>();
			mockTransactionService.Setup(x => x.RegisterExit(It.IsAny<RegisterTransactionRequest>()))
								  .ThrowsAsync(new InvalidOperationException("Insufficient funds")); 

			var controller = new TransactionController(mockTransactionService.Object);
			var request = new RegisterTransactionRequest { Amount = 100 };

			// Act
			var result = await controller.RegisterExit(request);

			// Assert
			var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			Assert.Equal("Insufficient funds", badRequestResult.Value);
		}
	}
}