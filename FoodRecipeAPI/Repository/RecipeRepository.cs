using FoodRecipeAPI.Data;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Recipe> GetAllRecipes()
        {
            return _context.Recipes.OrderBy(r => r.id).ToList();
        }

        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(r => r.id == id).FirstOrDefault();
        }

        public Recipe GetRecipe(string title)
        {
            return _context.Recipes.Where(r => r.title == title).FirstOrDefault();
        }

        public decimal GetRecipeRating(int id)
        {
            var rate = _context.Rates.Where(r => r.Recipe.id == id);
            if (rate.Count() <=0)
                return 0;
            return ((decimal)rate.Sum(s => s.score) / rate.Count());
        }

        bool IRecipeRepository.RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.id == id);
        }
    }
}
