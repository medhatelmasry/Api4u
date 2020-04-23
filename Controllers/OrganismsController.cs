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
    public class OrganismsController : ControllerBase
    {
        private readonly ToonsContext _context;

        public OrganismsController(ToonsContext context)
        {
            _context = context;
        }

        // GET: api/Organisms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organism>>> GetOrganism()
        {
            return await _context.Organism
            .ToListAsync();
        }

        // GET: api/Organisms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organism>> GetOrganism(int id)
        {
            var organism = await _context.Organism
            .FirstOrDefaultAsync(o => o.OrganismId == id);

            if (organism == null)
            {
                return NotFound();
            }

            return organism;
        }

        // PUT: api/Organisms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganism(int id, Organism organism)
        {
            if (id != organism.OrganismId)
            {
                return BadRequest();
            }

            _context.Entry(organism).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganismExists(id))
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

        // POST: api/Organisms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Organism>> PostOrganism(Organism organism)
        {
            _context.Organism.Add(organism);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganism", new { id = organism.OrganismId }, organism);
        }

        // DELETE: api/Organisms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organism>> DeleteOrganism(int id)
        {
            var organism = await _context.Organism.FindAsync(id);
            if (organism == null)
            {
                return NotFound();
            }

            _context.Organism.Remove(organism);
            await _context.SaveChangesAsync();

            return organism;
        }

        private bool OrganismExists(int id)
        {
            return _context.Organism.Any(e => e.OrganismId == id);
        }
    }
}
