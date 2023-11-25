using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetAllRecipes();
        Recipe GetRecipe(int id);
        Recipe GetRecipe(string title);
        decimal GetRecipeRating(int id);
        bool RecipeExists(int id);
    }
}
