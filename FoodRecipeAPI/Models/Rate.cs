namespace FoodRecipeAPI.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public double score { get; set; }
        public string comment { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
