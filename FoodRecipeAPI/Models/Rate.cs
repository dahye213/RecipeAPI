using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodRecipeAPI.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public double score { get; set; } // changed to double "No store type was specified for the decimal property score" warning
        public string comment { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public ICollection<Recipe> Recipes { get; set; } // So DeleteRateAsync works
    }
}
