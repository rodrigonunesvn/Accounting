using Accounting.TransactionProcessor.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Accounting.TransactionProcessor
{
	public class MongoDbDataAccess : IMongoDbDataAccess
	{
		private readonly IMongoDatabase _database;

		public MongoDbDataAccess(string connectionString, string databaseName)
		{
			var client = new MongoClient(connectionString);
			_database = client.GetDatabase(databaseName);
		}

		public async Task UpdateDailyBalance(DateTime currentDate, decimal dailyTotal)
		{
			var collection = _database.GetCollection<BsonDocument>("DailyBalances");

			var filter = Builders<BsonDocument>.Filter.Eq("Date", currentDate.Date);
			var update = Builders<BsonDocument>.Update.Set("Total", dailyTotal);

			var options = new UpdateOptions { IsUpsert = true };

			await collection.UpdateOneAsync(filter, update, options);
		}
	}
}
