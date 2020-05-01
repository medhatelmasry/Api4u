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

namespace Api4u.Controllers.Countries
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class CountriesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public CountriesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
            return await _context.Countries
            .Include(c => c.Provinces)
            .ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(string id)
        {
            var country = await _context.Countries
            .Include(c => c.Provinces)
            .FirstOrDefaultAsync(i => i.CountryName == id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(string id, Country country)
        {
            if (string.IsNullOrEmpty(country.CountryName)
                || string.IsNullOrEmpty(country.CapitalCity)
                || string.IsNullOrEmpty(country.ContinentName)
            ) return BadRequest("CountryName, CapitalCity and ContinentName are required.");

            if (id != country.CountryName)
            {
                return BadRequest();
            }

            country.CountryName = System.Net.WebUtility.HtmlEncode(country.CountryName);
            country.CapitalCity = System.Net.WebUtility.HtmlEncode(country.CapitalCity);
            country.ContinentName = System.Net.WebUtility.HtmlEncode(country.ContinentName);

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            if (string.IsNullOrEmpty(country.CountryName)
                || string.IsNullOrEmpty(country.CapitalCity)
                || string.IsNullOrEmpty(country.ContinentName)
            ) return BadRequest("CountryName, CapitalCity and ContinentName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Countries.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            country.CountryName = System.Net.WebUtility.HtmlEncode(country.CountryName);
            country.CapitalCity = System.Net.WebUtility.HtmlEncode(country.CapitalCity);
            country.ContinentName = System.Net.WebUtility.HtmlEncode(country.ContinentName);

            _context.Countries.Add(country);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CountryExists(country.CountryName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCountry", new { id = country.CountryName }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(string id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return country;
        }

        private bool CountryExists(string id)
        {
            return _context.Countries.Any(e => e.CountryName == id);
        }
    }
}
