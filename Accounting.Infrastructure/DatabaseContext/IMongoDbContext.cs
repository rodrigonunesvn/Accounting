using Accounting.Core.DTO.Response;
using MongoDB.Driver;

public interface IMongoDbContext
{
	IMongoCollection<DailyBalanceReportResponse> DailyBalances { get; }
}