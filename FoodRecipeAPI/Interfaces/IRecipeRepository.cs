using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Models;
using System.Linq.Expressions;

namespace FoodRecipeAPI.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();
        Task<Recipe> GetByIdAsync(int id);
        Task<Recipe> GetByTitleAsync(string title);
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task<bool> DeleteRecipeAsync(int id);
        Task<bool> RecipeExistsAsync(int id);
        Task<bool> SaveAsync();
        Task<decimal> GetRecipeRating(int id);


    }
}
