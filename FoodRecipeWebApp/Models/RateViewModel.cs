

namespace FoodRecipeWebApp.Models
{
    public class RateViewModel
    {
        public int Id { get; set; }
        public double score { get; set; }
        public string comment { get; set; }
        public int RecipeId { get; set; }
    }
}
