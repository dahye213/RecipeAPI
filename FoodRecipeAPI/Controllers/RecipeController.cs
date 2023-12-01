using AutoMapper;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace FoodRecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILogger<RecipeController> _logger;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper, ILogger<RecipeController> logger)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET ALL Recipes (It will return the list of all the recipes)
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Recipe>))]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetAllRecipes()
        {
            try
            {
                var recipes = await _recipeRepository.GetRecipesAsync();
                var results = _mapper.Map<IEnumerable<RecipeDto>>(recipes);
                return Ok(results);
            } catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        // GET a specific recipe
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type= typeof(Recipe))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RecipeDto>> GetRecipeById(int id)
        {
            try
            {
                var recipe = await _recipeRepository.GetByIdAsync(id);
                if (recipe == null)
                {
                    return NotFound($"Recipe with id {id} not found.");
                }
                var result = _mapper.Map<RecipeDto>(recipe);
                return Ok(result);
            } catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
        [HttpGet("title/{title}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RecipeDto>> GetRecipeByTitle(string title)
        {
            try
            {
                var recipe = await _recipeRepository.GetByTitleAsync(title);
                if (recipe == null)
                {
                    return NotFound($"Recipe with title {title} not found.");
                }
                var result = _mapper.Map<RecipeDto>(recipe);
                return Ok(result);
            } catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        // Get rating of a specific recipe
        [HttpGet("{id}/rating")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetRecipeRating(int id)
        {
            if(!await _recipeRepository.RecipeExistsAsync(id))
                return NotFound();

            var rating = await _recipeRepository.GetRecipeRating(id);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateRecipe([FromBody] RecipeDto recipeCreate)
        {
           try
            {
                if (await _recipeRepository.RecipeExistsAsync(recipeCreate.id))
                {
                    return StatusCode(409, $"Recipe with id {recipeCreate.id} already exists.");
                }
                var recipeToAdd = _mapper.Map<Recipe>(recipeCreate);
                await _recipeRepository.AddRecipeAsync(recipeToAdd);
                await _recipeRepository.SaveAsync();

                return StatusCode(201, "Recipe is successfully added");
            } catch (Exception ex)
            {
                // Log the exception message and stack trace for debugging
                _logger.LogError(ex, "Error creating recipe");

                // Optionally, return a more detailed error message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateRecipe(int recipeId, [FromBody] RecipeDto updatedRecipe)
        {
            try
            {
                if (!await _recipeRepository.RecipeExistsAsync(recipeId))
                    return NotFound();

                var recipeToUpdate = _mapper.Map<Recipe>(updatedRecipe);
                await _recipeRepository.UpdateRecipeAsync(recipeToUpdate);
                await _recipeRepository.SaveAsync();

                return Ok("Recipe is updated successfully.");
            } catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateServings(int id, [FromBody] RecipeDto patchRecipe)
        {
            try
            {
                if (!await _recipeRepository.RecipeExistsAsync(id))
                {
                    return NotFound($"Recipe with id {id} not found.");
                }

                var exstingRecipe = await _recipeRepository.GetByIdAsync(id);
                if (exstingRecipe == null)
                {
                    return NotFound($"Recipe with id {id} not found.");
                }
                _mapper.Map(patchRecipe, exstingRecipe);
                _recipeRepository.UpdateRecipeAsync(exstingRecipe);
                await _recipeRepository.SaveAsync();
                return Ok("Recipe is updated successfully.");
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
                
        }
       
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteRecipe(int id)
        {
            try
            {
                if (!await _recipeRepository.RecipeExistsAsync(id))
                {
                    return NotFound();
                }
                await _recipeRepository.DeleteRecipeAsync(id);
                return Ok("Recipe is successfully deleted");
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting recipe");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
