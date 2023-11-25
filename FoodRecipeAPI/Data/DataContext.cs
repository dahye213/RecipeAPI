using FoodRecipeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options) 
        {

        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient>Ingredients { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasKey(i => new { i.Id, i.RecipeId });
            modelBuilder.Entity<Ingredient>().HasOne(io => io.Recipe).WithMany(i => i.Ingredients).HasForeignKey(i => i.RecipeId);
        }
    }
}
