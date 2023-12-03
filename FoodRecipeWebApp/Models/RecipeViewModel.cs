using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodRecipeWebApp.Models
{
    public class RecipeViewModel
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Recipe Title")]
        public string title { get; set; }
        [Required]
        public string ingredients { get; set; }
        [Required]
        public string servings { get; set; }
        [Required]
        public string instructions { get; set; }
    }
}
