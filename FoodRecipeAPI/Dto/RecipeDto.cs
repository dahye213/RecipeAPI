using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Dto
{
    public class RecipeDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string ingredients { get; set; }
        public string servings { get; set; }
        public string instructions { get; set; }
    }
}
