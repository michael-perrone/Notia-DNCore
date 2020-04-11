using Microsoft.EntityFrameworkCore;
using Notia.Models;

namespace Notia.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        
        public DbSet<Value> Values {get;set;}
    }
}