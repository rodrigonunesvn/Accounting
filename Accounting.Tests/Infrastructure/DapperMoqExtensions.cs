using Dapper;
using Moq;
using Moq.Dapper;
using System.Data;

namespace Accounting.Tests.Infrastructure
{
	public static class DapperMoqExtensions
	{
		public static Mock<IDbConnection> SetupDapperQueryFirstOrDefault<T>(this Mock<IDbConnection> mockConnection, T result)
		{
			mockConnection.SetupDapperAsync(c => c.QueryFirstOrDefaultAsync<T>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<IDbTransaction>(), It.IsAny<int?>(), It.IsAny<CommandType?>()))
				.ReturnsAsync(result);
			return mockConnection;
		}
	}
}
