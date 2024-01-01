using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Accounting.TransactionProcessor
{
    public class Function1
    {
		private static IConfiguration? _configuration;

		private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;

			var builder = new ConfigurationBuilder()
				.SetBasePath(Environment.CurrentDirectory)
				.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
				.AddEnvironmentVariables();

			_configuration = builder.Build();
		}

		[Function(nameof(Function1))]
        public async Task Run([ServiceBusTrigger("transaction ", Connection = "ServiceBusConnectionString")] ServiceBusReceivedMessage message)
        {
			var sqlConnectionString = _configuration.GetConnectionString("SqlServerConnectionString");
			var mongoConnectionString = _configuration.GetConnectionString("MongoDbConnectionString");

			var sqlServerDataAccess = new SqlServerDataAccess(sqlConnectionString);
			var mongoDbDataAccess = new MongoDbDataAccess(mongoConnectionString, "transaction");
			var transactionProcessor = new TransactionProcessor(sqlServerDataAccess, mongoDbDataAccess);

			try
			{
				await transactionProcessor.ProcessTransactions();
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error processing message: {ex.Message}");
				throw;
			}
		}
    }
}
