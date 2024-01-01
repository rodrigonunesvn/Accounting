using Accounting.Core.Enum;
using Accounting.Core.Models;

namespace Accounting.Application.Validation
{
    public class TransactionValidationService : ITransactionValidationService
	{
		public bool IsTransactionValid(Transaction transaction, decimal currentBalance)
		{
			if (transaction.TransactionType == TransactionTypeEnum.Entry)
			{
				return true;
			}
			else if (transaction.TransactionType == TransactionTypeEnum.Exit && currentBalance >= transaction.Amount)
			{
				return true;
			}

			return false;
		}
	}
}
