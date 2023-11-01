using Microsoft.Extensions.Configuration;
using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace CocktailAppBackend
{
    public class CocktailAppDBContext : DbContext
    {
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeDetail> RecipeDetails { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        private readonly IConfiguration _configuration;

        public CocktailAppDBContext(){}

        public CocktailAppDBContext(DbContextOptions<CocktailAppDBContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public async Task UpdateRecipeValues()
        {
            await UpdateRecipeAvailabilityBasedOnIngredients();
            await UpdateRecipeKcalInTotal();
        }

        public async Task UpdateRecipeAvailabilityBasedOnIngredients()
        {
            var recipes = await Recipes.Include(r => r.Ingredients).ToListAsync();

            foreach (var recipe in recipes)
            {
                bool isAvailable = true;
                foreach (var ingredient in recipe.Ingredients)
                {
                    if (!ingredient.InStorage)
                    {
                        isAvailable = false;
                        break;
                    }
                }
                recipe.IsAvailable = isAvailable;
            }

            await SaveChangesAsync();
        }

        public async Task UpdateRecipeKcalInTotal()
        {
            var recipes = await Recipes.Include(r => r.RecipeDetails).ThenInclude(rd => rd.Ingredient).ToListAsync();

            foreach (var recipe in recipes)
            {
                float kcalInTotal = 0;
                foreach (var recipeDetail in recipe.RecipeDetails)
                {
                    kcalInTotal += recipeDetail.AmountInOz * recipeDetail.Ingredient.Kcal;
                }
                recipe.KcalInTotal = kcalInTotal;
            }

            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Recipe-Ingredient n:m Beziehung über RecipeDetail
            modelBuilder.Entity<Recipe>()
                .HasMany(e => e.Ingredients)
                .WithMany(e => e.Recipes)
                .UsingEntity<RecipeDetail>();

            // Recipe-Tag n:m Beziehung über RecipeTag
            modelBuilder.Entity<Recipe>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Recipes)
                .UsingEntity<RecipeTag>();

            // Recipe-Order 1:n Beziehung über FK "RecipeId"
            modelBuilder.Entity<Order>()
                .HasOne(e => e.Recipe)
                .WithMany(e => e.Orders)
                .HasForeignKey("RecipeId")
                .IsRequired();

            // Auth-Order 1:n Beziehung über FK "CreatedByUserId"
            modelBuilder.Entity<Order>()
                .HasOne(e => e.CreatedByUser)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.SetNull);

            // Auth-Recipe n:m Beziehung über Favourite
            modelBuilder.Entity<Auth>()
                .HasMany(e => e.FavouritedRecipes)
                .WithMany(e => e.FavouritedByAuths)
                .UsingEntity<Favourite>();

            // Anpassung für Kaskadierung
            modelBuilder.Entity<Auth>()
                .HasMany(e => e.Favourites)
                .WithOne(e => e.FavouritedByAuth)
                .OnDelete(DeleteBehavior.Cascade);

            // Auth-Recipe n:m Beziehung über Rating
            modelBuilder.Entity<Auth>()
                .HasMany(e => e.RatedRecipes)
                .WithMany(e => e.RatedByAuths)
                .UsingEntity<Rating>();            

            // Anpassung für Kaskadierung
            modelBuilder.Entity<Auth>()
                .HasMany(e => e.Ratings)
                .WithOne(e => e.RatedBy)
                .OnDelete(DeleteBehavior.Cascade);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"), new MariaDbServerVersion("10.9.8"),
                options => {
                    options.EnableRetryOnFailure();
                });
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
