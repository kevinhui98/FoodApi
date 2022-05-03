#nullable disable
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace FoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly MyFirstAPIDBContext _context;

        public FoodController(MyFirstAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/Food
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFood()
        {
            
            return await _context.Food.Include("Ingredient").ToListAsync();
        }

        // GET: api/Food/id=5
        [HttpGet("id={id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _context.Food.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // GET: api/Food/vegan
        [HttpGet("vegan={val}")]
        public async Task<ActionResult<Food>> GetVegan(bool val)
        {
            if(val == true)
            {
                return await _context.Food.FindAsync(val);
            }
            return NotFound();
        }


        // PUT: api/Food/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            if (id != food.FoodId)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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
        

        /*[HttpPatch("{id}")]
        public async Task<IActionResult> PatchFood(int FoodId, [FromBody] JsonPatchDocument<Food> foodUpdates)
        {
            var food = await _context.Food.FindAsync(FoodId);
            if (food != null) 
            {
                foodUpdates.ApplyTo(food, ModelState); // Must have Microsoft.AspNetCore.Mvc.NewtonsoftJson installed
                await _context.SaveChangesAsync();
                return Ok();
            }

            
            return NotFound();
        }*/

        // POST: api/Food
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            _context.Food.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
        }

        //DELETE: api/food/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var food = await _context.Food.Include(c => c.Ingredient).FirstOrDefaultAsync(c => c.FoodId == id);
            if(food == null)
            {
                return NotFound();
            }
            _context.Food.Remove(food);
            var ingredient = await _context.Ingredient.FirstOrDefaultAsync(e => e.IngredientId == food.Ingredient.IngredientId);
            _context.Food.Remove(food);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.FoodId == id);
        }
    }
}
