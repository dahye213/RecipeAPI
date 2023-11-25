using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        public RecipeController(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }
        // GET ALL Recipes (It will return the list of all the recipes)
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Recipe>))]
        public IActionResult GetAllRecipes()
        {
            var recipes = _recipeRepository.GetAllRecipes();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            return Ok(recipes);
        }

        // GET a specific recipe
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type= typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int id)
        {
            if(!_recipeRepository.RecipeExists(id))
                return NotFound();

            var recipe = _recipeRepository.GetRecipe(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(recipe); 
        }

        // Get rating of a specific recipe
        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeRating(int id)
        {
            if(!_recipeRepository.RecipeExists(id))
                return NotFound();

            var rating = _recipeRepository.GetRecipeRating(id);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }
    }
}
