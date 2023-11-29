using AutoMapper;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace FoodRecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IRateRepository _rateRepository;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeRepository recipeRepository, IRateRepository rateRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _rateRepository = rateRepository;
            _mapper = mapper;
        }
        // GET ALL Recipes (It will return the list of all the recipes)
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Recipe>))]
        public IActionResult GetAllRecipes()
        {
            var recipes = _recipeRepository.GetRecipes();

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromBody] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
                return BadRequest(ModelState);
            var recipe = _recipeRepository.GetRecipeTrimToUpper(recipeCreate);
            if (recipe != null)
            {
                ModelState.AddModelError("", "Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);
            if (!_recipeRepository.CreateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRecipe(int recipeId, [FromBody] RecipeDto updatedRecipe)
        {
            if (updatedRecipe == null)
                return BadRequest(ModelState);
            if (recipeId != updatedRecipe.id)
                return BadRequest(ModelState);
            if(!_recipeRepository.RecipeExists(recipeId))
                return NotFound();
            if(!ModelState.IsValid)
                return BadRequest();

            var recipeMap = _mapper.Map<Recipe>(updatedRecipe);
            if (!_recipeRepository.UpdateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /*
        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
            {
                return NotFound();
            }

            var ratesToDelete = _rateRepository.GetRate(recipeId); // need a function that will search for rates for specific recipe
            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_rateRepository.DeleteRates(ratesToDelete.ToList(ratesToDelete))) // After George creating DeleteRates Method, It'll be resolved
            {
                ModelState.AddModelError("", "Something went wrong when deleting rates");
            }
            if (!_recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting recipe");
            }

            return NoContent();

        }
        */
    }
}
