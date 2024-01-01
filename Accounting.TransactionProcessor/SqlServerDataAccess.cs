using Accounting.TransactionProcessor.Interfaces;
using Microsoft.Data.SqlClient;

namespace Accounting.TransactionProcessor
{
	public class SqlServerDataAccess : ISqlServerDataAccess
	{
		private readonly string _connectionString;

		public SqlServerDataAccess(string connectionString)
		{
			_connectionString = connectionString;
		}

		public decimal GetDailyTotal(DateTime currentDate)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				connection.Open();

				var query = "SELECT SUM(Amount) FROM Transactions WHERE TransactionDate = @CurrentDate";

				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@CurrentDate", currentDate.Date);

					var result = command.ExecuteScalar();

					if (result != DBNull.Value && result != null)
					{
						return Convert.ToDecimal(result);
					}

					return 0; 
				}
			}
		}
	}
}
