using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace FoodRecipeAPI.Models
{
    public class Recipe
    {
        public int id { get; set; }
        public string title { get; set; }
        public string ingredients { get; set; }
        public string servings { get; set; }
        public string instructions { get; set; }
        public ICollection<Rate> Rates { get; set; }

    }

}
