using LyncasSales.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace LyncasSales.Domain.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser,  IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<SaleItem> SaleItem { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Customer> Customers { get; set; }
      

    }
}