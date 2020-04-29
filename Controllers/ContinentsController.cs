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
    public class ContinentsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public ContinentsController(IConfiguration configuration,ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Continents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Continent>>> GetContinent()
        {
            return await _context.Continents
            .Include(c => c.Countries)
            .ToListAsync();
        }

        // GET: api/Continents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Continent>> GetContinent(string id)
        {
            var continent = await _context.Continents
            .Include(c => c.Countries)
            .FirstOrDefaultAsync(i => i.ContinentName == id);

            if (continent == null)
            {
                return NotFound();
            }

            return continent;
        }

        // PUT: api/Continents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContinent(string id, Continent continent)
        {
            if (string.IsNullOrEmpty(continent.ContinentName)
            ) return BadRequest("ContinentName is required.");

            if (id != continent.ContinentName)
            {
                return BadRequest();
            }

            continent.ContinentName = System.Net.WebUtility.HtmlEncode(continent.ContinentName);
    
            _context.Entry(continent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
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

        // POST: api/Continents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Continent>> PostContinent(Continent continent)
        {
            if (string.IsNullOrEmpty(continent.ContinentName)
            ) return BadRequest("ContinentName is required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Continents.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            continent.ContinentName = System.Net.WebUtility.HtmlEncode(continent.ContinentName);
    
            _context.Continents.Add(continent);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContinentExists(continent.ContinentName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContinent", new { id = continent.ContinentName }, continent);
        }

        // DELETE: api/Continents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Continent>> DeleteContinent(string id)
        {
            var continent = await _context.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continents.Remove(continent);
            await _context.SaveChangesAsync();

            return continent;
        }

        private bool ContinentExists(string id)
        {
            return _context.Continents.Any(e => e.ContinentName == id);
        }
    }
}
