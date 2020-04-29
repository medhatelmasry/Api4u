using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Species;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class OrganismsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public OrganismsController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Organisms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organism>>> GetOrganism()
        {
            return await _context.Organisms
            .ToListAsync();
        }

        // GET: api/Organisms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organism>> GetOrganism(int id)
        {
            var organism = await _context.Organisms
            .FindAsync(id);

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
            if (string.IsNullOrEmpty(organism.Name)
                || string.IsNullOrEmpty(organism.SpecieName)
            ) return BadRequest("Name and SpecieName are required.");

            if (id != organism.OrganismId)
            {
                return BadRequest();
            }

            organism.Name = System.Net.WebUtility.HtmlEncode(organism.Name);
            organism.SpecieName = System.Net.WebUtility.HtmlEncode(organism.SpecieName);

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
            if (string.IsNullOrEmpty(organism.Name)
                || string.IsNullOrEmpty(organism.SpecieName)
            ) return BadRequest("Name and SpecieName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Organisms.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            organism.Name = System.Net.WebUtility.HtmlEncode(organism.Name);
            organism.SpecieName = System.Net.WebUtility.HtmlEncode(organism.SpecieName);

            _context.Organisms.Add(organism);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganism", new { id = organism.OrganismId }, organism);
        }

        // DELETE: api/Organisms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organism>> DeleteOrganism(int id)
        {
            var organism = await _context.Organisms.FindAsync(id);
            if (organism == null)
            {
                return NotFound();
            }

            _context.Organisms.Remove(organism);
            await _context.SaveChangesAsync();

            return organism;
        }

        private bool OrganismExists(int id)
        {
            return _context.Organisms.Any(e => e.OrganismId == id);
        }
    }
}
