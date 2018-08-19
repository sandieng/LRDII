using LRDII.Entities;
using LRDII.Models;
using Microsoft.EntityFrameworkCore;

namespace LRDII.Infrastructure
{
    public class LrdiiDbContext : DbContext
    {
        public DbSet<MemberModel> Members { get; set; }
        //public DbSet<Member> Members { get; set; }
        public DbSet<SharePriceModel> SharePrices { get; set; }
        public DbSet<ShareTransactionModel> ShareTransactions { get; set; }
        public DbSet<LoanTransactionModel> LoanTransactions { get; set; }
        public DbSet<LoanRepaymentTransactionModel> LoanRepaymentTransactions { get; set; }
        public DbSet<GoodsTransactionModel> GoodsTransactions { get; set; }


        public LrdiiDbContext(DbContextOptions<LrdiiDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
