using Accounting.Core.DTO.Response;
using MongoDB.Driver;

public class MongoDbContext : IMongoDbContext
{
	private readonly IMongoDatabase _database;

	public MongoDbContext(string connectionString, string databaseName)
	{
		var client = new MongoClient(connectionString);
		_database = client.GetDatabase(databaseName);
	}

	public IMongoCollection<DailyBalanceReportResponse> DailyBalances => _database.GetCollection<DailyBalanceReportResponse>("DailyBalances");
}
