using Microsoft.EntityFrameworkCore;
using Notia.Models;

namespace Notia.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        
        public DbSet<Value> Values {get;set;}
        public DbSet<User> Users {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
        modelBuilder.Entity<User>()
        .Property(b => b.NewUser)
        .HasDefaultValue(true);
}
    }
}