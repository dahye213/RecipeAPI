using System;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeAPI.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class RateController : Controller {
        private readonly IRateRepository _rateRepository;
        public RateController(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }
        // GET ALL Rate (It will return the list of all the ratings)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Rate>))]
        public IActionResult GetAllRate()
        {
            var rate = _rateRepository.GetAllRate();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rate);
        }

        // GET a specific rate
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Rate))]
        [ProducesResponseType(400)]
        public IActionResult GetRate(int Id)
        {
            if (!_rateRepository.RateExists(Id))
                return NotFound();

            var rate = _rateRepository.GetRate(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rate);
        }

        // Get recipe of a specific rating
        [HttpGet("{id}/recipe")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetRateRecipe(int Id)
        {
            if (!_rateRepository.RateExists(Id))
                return NotFound();

            var recipe = _rateRepository.GeteRateRecipe(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(recipe);
        }
    }
}
