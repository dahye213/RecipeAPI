using FoodRecipeAPI.Data;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FoodRecipeAPI.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context)
        {
            _context = context;
        }
       
        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRecipeAsync(int id)
        {
            var recipeToDelete = await _context.Recipes.Include(r => r.Rates).SingleAsync(r => r.id == id);
            var rateTemp = new List<Rate>();
            rateTemp.AddRange(recipeToDelete.Rates);
            if (recipeToDelete != null)
            {
                recipeToDelete.Rates.Clear();
                _context.Recipes.Remove(recipeToDelete);
                _context.Rates.RemoveRange(rateTemp);
            }

            return await SaveAsync();
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.id == id);
            if (recipe == null)
            {
                throw new Exception("The recipe does not exist");
            }
            return recipe;
        }

        public async Task<Recipe> GetByTitleAsync(string title)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.title == title);
            if (recipe == null)
            {
                throw new Exception("The recipe does not exist");
            }
            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return recipes.OrderBy(r => r.id);
        }

        public async Task<bool> RecipeExistsAsync(int id)
        {
            return await _context.Recipes.AnyAsync(r => r.id == id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await SaveAsync();
        }

        public async Task<decimal> GetRecipeRating(int id)
        {
            var rate = _context.Rates.Where(rt => rt.RecipeId == id);
            var rateCount = await rate.CountAsync();
            if (rateCount <= 0)
                return 0;
            var totalRating = await rate.SumAsync(rt => rt.score);
            return (decimal)totalRating / rateCount;
        }
    }
}
