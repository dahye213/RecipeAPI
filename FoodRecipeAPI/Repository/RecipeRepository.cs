using FoodRecipeAPI.Data;
using FoodRecipeAPI.Dto;
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

        public ICollection<Recipe> GetRecipes()
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

        public bool RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.id == id);
        }

        public bool CreateRecipe(Recipe recipe)
        {
            _context.Add(recipe);
            return Save();
        }

        public bool Save()
        {
            // It saves changes 
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Recipe GetRecipeTrimToUpper(RecipeDto recipeCreate)
        {
            return GetRecipes().Where(r => r.title.Trim().ToUpper() == recipeCreate.title.TrimEnd().ToUpper()).FirstOrDefault();
        }

        public bool UpdateRecipe(Recipe recipe)
        {
            _context.Update(recipe);
            return Save();
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }
    }
}
