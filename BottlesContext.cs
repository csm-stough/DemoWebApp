using Microsoft.EntityFrameworkCore;
using DemoWebApp.Models;

namespace DemoWebApp
{
    public class BottlesContext : DbContext
    {
        public DbSet<BottlesModel> Bottles { get; set; }
        public string DbPath { get; }

        public BottlesContext(IConfiguration configuration)
        {
            DbPath = configuration.GetConnectionString("ConnectionString");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DbPath);
        }
    }
}