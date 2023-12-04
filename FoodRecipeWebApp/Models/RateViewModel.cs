using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodRecipeWebApp.Models
{
    public class RateViewModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Ratings")]
        public double score { get; set; }
        [Required]
        public string comment { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
