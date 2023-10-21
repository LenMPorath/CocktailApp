using Microsoft.Extensions.Configuration;
using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CocktailAppBackend
{
    public class CocktailAppDBContext : DbContext
    {
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeDetail> RecipeDetails { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        private readonly IConfiguration _configuration;

        public CocktailAppDBContext()
        {
        }

        public CocktailAppDBContext(DbContextOptions<CocktailAppDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(e => e.Ingredients)
                .WithMany(e => e.Recipes)
                .UsingEntity(
                   "RecipeDetail",
                    l => l.HasOne(typeof(Ingredient)).WithMany().HasForeignKey("IngredientsId").HasPrincipalKey(nameof(Ingredient.Id)),
                    r => r.HasOne(typeof(Recipe)).WithMany().HasForeignKey("RecipesId").HasPrincipalKey(nameof(Recipe.Id)),
                    j => j.HasKey("RecipesId", "IngredientsId"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MariaDbServerVersion("10.9.8");
            optionsBuilder.UseMySql(connectionString, serverVersion,
                options => options.EnableRetryOnFailure());
        }

    }
}
