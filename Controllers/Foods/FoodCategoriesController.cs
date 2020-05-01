using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Foods;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Foods
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class FoodCategoriesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public FoodCategoriesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/FoodCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodCategory>>> GetFoodCategories()
        {
            return await _context.FoodCategories
            .Include(f => f.Foods)
            .ToListAsync();
        }

        // GET: api/FoodCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodCategory>> GetFoodCategory(int id)
        {
            var foodCategory = await _context.FoodCategories
            .Include(f => f.Foods)
            .FirstOrDefaultAsync(i => i.FoodCategoryId == id);

            if (foodCategory == null)
            {
                return NotFound();
            }

            return foodCategory;
        }

        // PUT: api/FoodCategories/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodCategory(int id, FoodCategory foodCategory)
        {
            if (string.IsNullOrEmpty(foodCategory.Name)
                || string.IsNullOrEmpty(foodCategory.Description)
            ) return BadRequest("Name and description are required.");

            if (id != foodCategory.FoodCategoryId)
            {
                return BadRequest();
            }

            foodCategory.Name = System.Net.WebUtility.HtmlEncode(foodCategory.Name);
            foodCategory.Description = System.Net.WebUtility.HtmlEncode(foodCategory.Description);

            _context.Entry(foodCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodCategoryExists(id))
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

        // POST: api/FoodCategories
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FoodCategory>> PostFoodCategory(FoodCategory foodCategory)
        {
            if (string.IsNullOrEmpty(foodCategory.Name)
                || string.IsNullOrEmpty(foodCategory.Description)
            ) return BadRequest("Name and description are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.FoodCategories.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            foodCategory.Name = System.Net.WebUtility.HtmlEncode(foodCategory.Name);
            foodCategory.Description = System.Net.WebUtility.HtmlEncode(foodCategory.Description);

            _context.FoodCategories.Add(foodCategory);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoodCategory", new { id = foodCategory.FoodCategoryId }, foodCategory);
        }

        // DELETE: api/FoodCategories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodCategory>> DeleteFoodCategory(int id)
        {
            var foodCategory = await _context.FoodCategories.FindAsync(id);
            if (foodCategory == null)
            {
                return NotFound();
            }

            _context.FoodCategories.Remove(foodCategory);
            await _context.SaveChangesAsync();

            return foodCategory;
        }

        private bool FoodCategoryExists(int id)
        {
            return _context.FoodCategories.Any(e => e.FoodCategoryId == id);
        }
    }
}
