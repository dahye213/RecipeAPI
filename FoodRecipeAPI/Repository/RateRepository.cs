using System;
using FoodRecipeAPI.Data;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Repository
{
	public class RateRepository : IRateRepository
    {
        private readonly DataContext _context;

        public RateRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Rate> GetAllRate()
        {
            return _context.Rates.OrderBy(r => r.Id).ToList();
        }

        public Rate GetRate(int Id)
        {
            return _context.Rates.Where(r => r.Id == Id).FirstOrDefault();
        }

        public Rate GetRate(decimal score)
        {
            return _context.Rates.Where(r => r.score == score).FirstOrDefault();
        }
        public decimal GetRateRecipe(int RecipeId)
        {
            var recipe = _context.Rates.Where(r => r.RecipeId == RecipeId);
            if (recipe.Count() <= 0)
                return 0;
            return ((decimal)recipe.Sum(s => s.score) / recipe.Count());
        }

        bool IRateRepository.RateExists(decimal score)
        {
            return _context.Rates.Any(r => r.score == score);
        }
    }
}
