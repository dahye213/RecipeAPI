using System;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Interfaces
{
	public interface IRateRepository {
        ICollection<Rate> GetAllRate();
        Rate GetRate(int id);
        Rate GetRate(decimal score);
        Rate GetRate(string comment);
        decimal GetRateRecipe(int RecipeId);
        bool RateExists(int id);
    }
}
