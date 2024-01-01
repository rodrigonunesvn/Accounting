using Accounting.Core.Enum;

namespace Accounting.Core.Models
{
    public class Transaction
    {
        public Transaction(decimal Amount, TransactionTypeEnum TransactionType) { 
            this.Amount = Amount; this.TransactionType = TransactionType; this.TransactionDate = DateTime.UtcNow;
        }

        public int TransactionId { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
