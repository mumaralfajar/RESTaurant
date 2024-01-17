using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTaurant.Models;
using RESTaurant.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodService _foodService;

        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

        /// <summary>
        /// Retrieves all foods.
        /// </summary>
        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods()
        {
            return await _foodService.GetFoods();
        }

        /// <summary>
        /// Retrieves food by id.
        /// </summary>
        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _foodService.GetFood(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        /// <summary>
        /// Updates food.
        /// </summary>
        // PUT: api/Foods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            if (id != food.FoodId)
            {
                return BadRequest();
            }

            try
            {
                await _foodService.UpdateFood(id, food);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_foodService.FoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adds food.
        /// </summary>
        // POST: api/Foods
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            await _foodService.AddFood(food);
            return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
        }

        /// <summary>
        /// Deletes food by id.
        /// </summary>
        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _foodService.DeleteFood(id);
            if (food == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}