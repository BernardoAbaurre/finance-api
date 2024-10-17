using finance_api.Domain.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace finance_api.Infra.DbContexts
{
    public class FinanceDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("FINANCE");
            base.OnModelCreating(modelBuilder);
        }
    }
}
