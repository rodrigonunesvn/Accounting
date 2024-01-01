using Accounting.Core.Models;

namespace Accounting.Application.Validation
{
    public interface ITransactionValidationService
    {
        bool IsTransactionValid(Transaction transaction, decimal currentBalance);		
	}
}
