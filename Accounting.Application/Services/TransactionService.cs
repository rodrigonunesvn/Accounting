using Accounting.Application.Validation;
using Accounting.Core.DTO.Request;
using Accounting.Core.Models;
using Accounting.Core.Services;
using Accounting.Infrastructure;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Accounting.Application.Services
{
	public class TransactionService : ITransactionService
	{
		private readonly ITransactionRepository _transactionRepository;
		private readonly ITransactionValidationService _transactionValidationService;
		private readonly IQueueClient _queueClient;

		public TransactionService(ITransactionRepository transactionRepository, IQueueClient queueClient, ITransactionValidationService transactionValidationService)
		{
			_transactionRepository = transactionRepository;
			_queueClient = queueClient;
			_transactionValidationService = transactionValidationService;
		}

		public async Task RegisterEntry(RegisterTransactionRequest request)
		{
			var transaction = new Transaction(request.Amount, Core.Enum.TransactionTypeEnum.Entry);

			await RegisterTransaction(transaction);
		}

		public async Task RegisterExit(RegisterTransactionRequest request)
		{
			var transaction = new Transaction(request.Amount, Core.Enum.TransactionTypeEnum.Exit);

			await RegisterTransaction(transaction);
		}

		private async Task RegisterTransaction(Transaction transaction)
		{
			decimal currentBalance = await _transactionRepository.GetCurrentBalance();

			if (_transactionValidationService.IsTransactionValid(transaction, currentBalance))
			{
				var transactionAdded = await _transactionRepository.AddTransaction(transaction);

				if(!transactionAdded)
				{
					throw new InvalidOperationException("Error adding transaction.");
				}

				await SendTransactionToQueue(transaction);
			}
			else
			{
				throw new InvalidOperationException("Invalid transaction or insufficient funds.");
			}
		}		

		private async Task SendTransactionToQueue(Transaction transaction)
		{
			string messageBody = JsonConvert.SerializeObject(transaction);
			var message = new Message(Encoding.UTF8.GetBytes(messageBody));

			await _queueClient.SendAsync(message);
		}
	}
}
