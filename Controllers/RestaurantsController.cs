using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Sports;
using Microsoft.Extensions.Configuration;
using Api4u.Models.Restaurants;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class RestaurantsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public RestaurantsController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Restaurants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            return await _context.Restaurants
            .Include(t => t.MenuItems)
            .ToListAsync();
        }

        // GET: api/Restaurants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var rstaurant = await _context.Restaurants
            .Include(t => t.MenuItems)
            .FirstOrDefaultAsync(i => i.RestaurantId == id); ;

            if (rstaurant == null)
            {
                return NotFound();
            }

            return rstaurant;
        }

        // PUT: api/Restaurants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (string.IsNullOrEmpty(restaurant.RestaurantName)
                || string.IsNullOrEmpty(restaurant.Street)
                || string.IsNullOrEmpty(restaurant.City)
                || string.IsNullOrEmpty(restaurant.Province)
                || string.IsNullOrEmpty(restaurant.Country)
                || string.IsNullOrEmpty(restaurant.FoodType)
            ) return BadRequest("RestaurantName, Street, City, Province Country and FoodType are required.");

            if (id != restaurant.RestaurantId)
            {
                return BadRequest();
            }

            restaurant.Street = System.Net.WebUtility.HtmlEncode(restaurant.Street);
            restaurant.Country = System.Net.WebUtility.HtmlEncode(restaurant.Country);
            restaurant.City = System.Net.WebUtility.HtmlEncode(restaurant.City);
            restaurant.RestaurantName = System.Net.WebUtility.HtmlEncode(restaurant.RestaurantName);
            restaurant.Province = System.Net.WebUtility.HtmlEncode(restaurant.Province);
            restaurant.FoodType = System.Net.WebUtility.HtmlEncode(restaurant.FoodType);

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
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

        // POST: api/Restaurants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            if (string.IsNullOrEmpty(restaurant.RestaurantName)
                || string.IsNullOrEmpty(restaurant.Street)
                || string.IsNullOrEmpty(restaurant.City)
                || string.IsNullOrEmpty(restaurant.Province)
                || string.IsNullOrEmpty(restaurant.Country)
                || string.IsNullOrEmpty(restaurant.FoodType)
            ) return BadRequest("RestaurantName, Street, City, Province Country and FoodType are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Restaurants.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            restaurant.Street = System.Net.WebUtility.HtmlEncode(restaurant.Street);
            restaurant.Country = System.Net.WebUtility.HtmlEncode(restaurant.Country);
            restaurant.City = System.Net.WebUtility.HtmlEncode(restaurant.City);
            restaurant.RestaurantName = System.Net.WebUtility.HtmlEncode(restaurant.RestaurantName);
            restaurant.Province = System.Net.WebUtility.HtmlEncode(restaurant.Province);
            restaurant.FoodType = System.Net.WebUtility.HtmlEncode(restaurant.FoodType);

            _context.Restaurants.Add(restaurant);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RestaurantExists(restaurant.RestaurantId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRestaurant", new { id = restaurant.RestaurantId }, restaurant);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurants(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return restaurant;
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.RestaurantId == id);
        }
    }
}
