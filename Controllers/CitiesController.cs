using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Countries;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class CitiesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public CitiesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.Cities
            .ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(string id)
        {
            var city = await _context.Cities
                .FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(string id, City city)
        {
            if (string.IsNullOrEmpty(city.CityName)
            ) return BadRequest("CityName is required.");

            if (id != city.CityName)
            {
                return BadRequest();
            }

            city.CityName = System.Net.WebUtility.HtmlEncode(city.CityName);

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            if (string.IsNullOrEmpty(city.CityName)
            ) return BadRequest("CityName is required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Cities.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            city.CityName = System.Net.WebUtility.HtmlEncode(city.CityName);

            _context.Cities.Add(city);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CityExists(city.CityName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCity", new { id = city.CityName }, city);
        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(string id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return city;
        }

        private bool CityExists(string id)
        {
            return _context.Cities.Any(e => e.CityName == id);
        }
    }
}
