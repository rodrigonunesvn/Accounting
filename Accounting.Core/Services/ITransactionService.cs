using Accounting.Core.DTO.Request;
using Accounting.Core.Models;

namespace Accounting.Core.Services
{
    public interface ITransactionService
	{
		Task RegisterEntry(RegisterTransactionRequest request);

		Task RegisterExit(RegisterTransactionRequest request);

		Task<decimal> GetCurrentBalance();
	}
}
