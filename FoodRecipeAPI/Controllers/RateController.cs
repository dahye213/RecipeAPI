using AutoMapper;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using FoodRecipeAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
namespace FoodRecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Controller
    {
        private readonly IRateRepository _rateRepository;
        private readonly ILogger<RateController> _logger;
        private readonly IMapper _mapper;
        public RateController(IRateRepository rateRepository, IMapper mapper, ILogger<RateController> logger)
        {
            _rateRepository = rateRepository;
            _mapper = mapper;
            _logger = logger;
        }


        // GET ALL Rate (It will return the list of all the recipes)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rate>))]
        public async Task<ActionResult<IEnumerable<Rate>>> GetAllRate()
        {
            try
            {
                var rates = await _rateRepository.GetRateAsync();
                var results = _mapper.Map<IEnumerable<RateDto>>(rates);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        // GET a specific rate
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Rate))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RateDto>> GetRateById(int Id)
        {
            try
            {
                var rate = await _rateRepository.GetByIdAsync(Id);
                if (rate == null)
                {
                    return NotFound($"Rate with id {Id} not found.");
                }
                var result = _mapper.Map<RateDto>(rate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }

        [HttpGet("score/{score}")]
        [ProducesResponseType(200, Type = typeof(Rate))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RateDto>> GetRateByScore(double score)
        {
            try
            {
                var recipe = await _rateRepository.GetByScoreAsync(score);
                if (recipe == null)
                {
                    return NotFound($"Rate with a score of {score} not found.");
                }
                var result = _mapper.Map<RateDto>(score);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Post Rate
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateRate([FromBody] RateDto rateCreate)
        {
            try
            {
                if (await _rateRepository.RateExistsAsync(rateCreate.Id))
                {
                    return StatusCode(409, $"Rate with id {rateCreate.Id} already exists.");
                }
                var rateToAdd = _mapper.Map<Rate>(rateCreate);
                await _rateRepository.AddRateAsync(rateToAdd);
                await _rateRepository.SaveAsync();

                return StatusCode(201, "Rate is successfully added");
            }
            catch (Exception ex)
            {
                // Log the exception message and stack trace for debugging
                _logger.LogError(ex, "Error creating rating");

                // Optionally, return a more detailed error message
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Put recipeId
        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateRate(int recipeId, [FromBody] RateDto updatedRate)
        {
            try
            {
                if (!await _rateRepository.RateExistsAsync(recipeId))
                    return NotFound();

                var rateToUpdate = _mapper.Map<Rate>(updatedRate);
                await _rateRepository.UpdateRateAsync(rateToUpdate);
                await _rateRepository.SaveAsync();

                return Ok("Rate is updated successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // Patch Rate
        [HttpPatch("{Id}")]
        public async Task<ActionResult> UpdateServings(int Id, [FromBody] RateDto patchRate)
        {
            try
            {
                if (!await _rateRepository.RateExistsAsync(Id))
                {
                    return NotFound($"Rate with id {Id} not found.");
                }

                var exstingRate = await _rateRepository.GetByIdAsync(Id);
                if (exstingRate == null)
                {
                    return NotFound($"Rate with id {Id} not found.");
                }
                _mapper.Map(patchRate, exstingRate);
                _rateRepository.UpdateRateAsync(exstingRate);
                await _rateRepository.SaveAsync();
                return Ok("Rate is updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        // Delete Rate via Id
        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteRate(int Id)
        {
            try
            {
                if (!await _rateRepository.RateExistsAsync(Id))
                {
                    return NotFound();
                }
                await _rateRepository.DeleteRateAsync(Id);
                return Ok("Rete is successfully deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting rate");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
