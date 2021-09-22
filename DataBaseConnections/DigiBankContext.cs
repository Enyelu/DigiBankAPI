using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataBaseConnections
{
    public class DigiBankContext:IdentityDbContext<User>
    {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<UserAddress> UsersAddress { get; set; }

        public DigiBankContext(DbContextOptions<DigiBankContext> options) : base(options)
        {
        }
    }
}
