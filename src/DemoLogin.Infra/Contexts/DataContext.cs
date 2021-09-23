using DemoLogin.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoLogin.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("Database");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
    }
}
