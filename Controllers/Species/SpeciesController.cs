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

namespace Api4u.Controllers.Species
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class SpeciesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public SpeciesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specie>>> GetSpecie()
        {
            return await _context.Species
            .Include(s => s.Organisms)
            .ToListAsync();
        }

        // GET: api/Species/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Specie>> GetSpecie(string id)
        {
            var specie = await _context.Species
            .Include(s => s.Organisms)
            .FirstOrDefaultAsync(i => i.SpecieName == id);

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
            if (string.IsNullOrEmpty(specie.SpecieName)
            ) return BadRequest("SpecieName IS required.");

            if (id != specie.SpecieName)
            {
                return BadRequest();
            }

            specie.SpecieName = System.Net.WebUtility.HtmlEncode(specie.SpecieName);

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
            if (string.IsNullOrEmpty(specie.SpecieName)
            ) return BadRequest("SpecieName IS required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Species.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            specie.SpecieName = System.Net.WebUtility.HtmlEncode(specie.SpecieName);

            _context.Species.Add(specie);
            
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
            var specie = await _context.Species.FindAsync(id);
            if (specie == null)
            {
                return NotFound();
            }

            _context.Species.Remove(specie);
            await _context.SaveChangesAsync();

            return specie;
        }

        private bool SpecieExists(string id)
        {
            return _context.Species.Any(e => e.SpecieName == id);
        }
    }
}
