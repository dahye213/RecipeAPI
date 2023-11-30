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
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Rate>()
                .HasOne(r=>r.Recipe)
                .WithMany(rt=>rt.Rates)
                .HasForeignKey(rt=>rt.RecipeId);

            modelBuilder.Entity<Recipe>()
                       .HasMany(r => r.Rates)
                       .WithOne(rt => rt.Recipe)
                       .HasPrincipalKey(r => r.id);
        }
    }
}
