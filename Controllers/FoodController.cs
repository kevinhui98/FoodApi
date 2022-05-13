using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodApi.Models;

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
        public async Task<ActionResult<FoodResponse>> GetFood()
        {
            var response = new FoodResponse();
            var food = await _context.Food.ToListAsync();

            if (food.Count == 0)
            {
                response.statusCode = 400;
                response.statusDescription = "Process Failed!!! ";

            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "Success!!!";
                response.food = food;
            }

            return response;
        }

        // GET: api/Food/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodResponse>> GetFood(int id)
        {
            var food = await _context.Food.FindAsync(id);
            var response = new FoodResponse();
            response.statusCode = 400;
            response.statusDescription = "Process Failed. food Does Not Exist";

            if (food != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Success. Loading Requested food";
                response.food.Add(food);
            }

            return response;
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

        // POST: api/Food
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodResponse>> PostFood(Food food)
        {
            var response = new FoodResponse();

            var result = _context.Food.Add(food);

            await _context.SaveChangesAsync();

            response.statusCode = 400;
            response.statusDescription = "Process Failed. Please Check Food Values";

            if (result != null)
            {
                response.statusCode = 200;
                response.statusDescription = "Success. New Food Added";
                response.food.Add(food);
            }

            return response;
        }

        // DELETE: api/Food/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodResponse>> DeleteFood(int id)
        {
            var response = new FoodResponse();

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                response.statusCode = 400;
                response.statusDescription = "Process Failed. food Does Not Exist.";
            }
            else
            {
                response.statusCode = 200;
                response.statusDescription = "Success. food Removed";
                _context.Food.Remove(food);
            }

            await _context.SaveChangesAsync();

            return response;
        }

        private bool FoodExists(int id)
        {
            return (_context.Food?.Any(e => e.FoodId == id)).GetValueOrDefault();
        }
    }
}