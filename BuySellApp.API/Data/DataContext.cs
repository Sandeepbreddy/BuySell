using BuySellApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BuySellApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Value> Values { get; set; }
    }
}