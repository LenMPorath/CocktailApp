using Microsoft.Extensions.Configuration;
using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CocktailAppBackend.Models
{
    public class SampleContext : DbContext
    {
        public DbSet<Sample> Samples { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("DefaultConnection", new MariaDbServerVersion("10.9.0"));
            }
        }
    }
}
