using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Species;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ToonsContext _context;

        public SpeciesController(ToonsContext context)
        {
            _context = context;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specie>>> GetSpecie()
        {
            return await _context.Specie.ToListAsync();
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specie>> GetSpecie(string id)
        {
            var specie = await _context.Specie.FindAsync(id);

            if (specie == null)
            {
                return NotFound();
            }

            return specie;
        }

        // PUT: api/Species/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecie(string id, Specie specie)
        {
            if (id != specie.SpecieName)
            {
                return BadRequest();
            }

            _context.Entry(specie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecieExists(id))
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

        // POST: api/Species
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Specie>> PostSpecie(Specie specie)
        {
            _context.Specie.Add(specie);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SpecieExists(specie.SpecieName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSpecie", new { id = specie.SpecieName }, specie);
        }

        // DELETE: api/Species/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Specie>> DeleteSpecie(string id)
        {
            var specie = await _context.Specie.FindAsync(id);
            if (specie == null)
            {
                return NotFound();
            }

            _context.Specie.Remove(specie);
            await _context.SaveChangesAsync();

            return specie;
        }

        private bool SpecieExists(string id)
        {
            return _context.Specie.Any(e => e.SpecieName == id);
        }
    }
}
