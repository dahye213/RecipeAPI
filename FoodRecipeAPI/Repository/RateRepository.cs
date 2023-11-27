using System;
using FoodRecipeAPI.Data;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Repository
{
	public class RateRepository : IRecipeRepository
    {
        private readonly DataContext _context;

        public RateRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Rate> GetAllRate()
        {
            return _context.Rate.OrderBy(r => r.Id).ToList();
        }

        public Rate GetRate(int Id)
        {
            return _context.Recipes.Where(r => r.Id == Id).FirstOrDefault();
        }

        public Rate GetRate(decimal score)
        {
            return _context.Recipes.Where(r => r.score == score).FirstOrDefault();
        }
        public decimal GetRateRecipe(int RecipeId)
        {
            var recipe = _context.Recipe.Where(r => r.Rate.RecipeId == RecipeId);
            if (recipe.Count() <= 0)
                return 0;
            return ((decimal)recipe.Sum(s => s.score) / recipe.Count());
        }

        bool IRateRepository.RateExists(decimal score)
        {
            return _context.Recipes.Any(r => r.score == score);
        }
    }
}
