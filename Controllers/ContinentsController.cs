using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Countries;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsController : ControllerBase
    {
        private readonly ToonsContext _context;

        public ContinentsController(ToonsContext context)
        {
            _context = context;
        }

        // GET: api/Continents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Continent>>> GetContinent()
        {
            return await _context.Continent.ToListAsync();
        }

        // GET: api/Continents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Continent>> GetContinent(string id)
        {
            var continent = await _context.Continent.FindAsync(id);

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
            if (id != continent.ContinentName)
            {
                return BadRequest();
            }

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
            _context.Continent.Add(continent);
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
            var continent = await _context.Continent.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continent.Remove(continent);
            await _context.SaveChangesAsync();

            return continent;
        }

        private bool ContinentExists(string id)
        {
            return _context.Continent.Any(e => e.ContinentName == id);
        }
    }
}
