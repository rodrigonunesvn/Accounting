using System;

namespace Accounting.Core.DTO.Response
{
	public class DailyBalanceReportResponse
    {
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
    }
}
