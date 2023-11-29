using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        Recipe GetRecipe(string title);
        Recipe GetRecipeTrimToUpper(RecipeDto recipeCreate);
        decimal GetRecipeRating(int id);
        bool RecipeExists(int id);
        bool CreateRecipe(Recipe recipe);
        bool UpdateRecipe(Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool Save();


      
    }
}
