namespace FoodRecipeAPI.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Vegetarian { get; set; }
        public bool Vegan { get; set; }
        public bool GlutenFree { get; set; }
        public bool DairyFree { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; } 
        public int ReadyInMinutes { get; set; }
        public string ImageUrl { get; set; }
        public string ImageType { get; set; }
        public string Instructions { get; set; }
    }

}
