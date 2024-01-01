using Accounting.Core.DTO.Request;
using Accounting.Core.Models;
using Accounting.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ITransactionService _transactionService;

		public TransactionController(ITransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpPost("Entry")]
		public async Task<IActionResult> RegisterEntry([FromBody] RegisterTransactionRequest request)
		{
			try
			{
				await _transactionService.RegisterEntry(request);
				return Ok("Entry registered successfully!");
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("Exit")]
		public async Task<IActionResult> RegisterExit([FromBody] RegisterTransactionRequest request)
		{
			try
			{
				await _transactionService.RegisterExit(request);
				return Ok("Entry registered successfully!");
			}
			catch (InvalidOperationException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("CurrentBalance")]
		public async Task<IActionResult> GetCurrentBalance()
		{
			try
			{
				var result = await _transactionService.GetCurrentBalance();

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Error while retrieving the current balance.: {ex.Message}");
			}
		}
	}
}
