using Accounting.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.DatabaseContext
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().HasKey(x => x.TransactionId);
        }
    }
}
