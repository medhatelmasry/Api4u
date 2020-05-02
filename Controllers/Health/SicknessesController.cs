using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api4u.Data;
using Api4u.Models.Health;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Health
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class SicknessesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public SicknessesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Sicknesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sickness>>> GetSicknesses()
        {
            return await _context.Sicknesses
            .Include(c => c.Medicines)
            .ToListAsync();
        }

        // GET: api/Sicknesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sickness>> GetSickness(int id)
        {
            var sickness = await _context.Sicknesses
            .Include(c => c.Medicines)
            .FirstOrDefaultAsync(i => i.SicknessId == id);

            if (sickness == null)
            {
                return NotFound();
            }

            return sickness;
        }

        // PUT: api/Sicknesses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSickness(int id, Sickness sickness)
        {
            if (string.IsNullOrEmpty(sickness.SicknessName)
            ) return BadRequest("SicknessName is required.");

            if (id != sickness.SicknessId)
            {
                return BadRequest();
            }

            sickness.SicknessName = System.Net.WebUtility.HtmlEncode(sickness.SicknessName);

            _context.Entry(sickness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SicknessExists(id))
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

        // POST: api/Sicknesses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sickness>> PostSickness(Sickness sickness)
        {
            if (string.IsNullOrEmpty(sickness.SicknessName)
            ) return BadRequest("SicknessName is required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Sicknesses.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            sickness.SicknessName = System.Net.WebUtility.HtmlEncode(sickness.SicknessName);

            _context.Sicknesses.Add(sickness);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SicknessExists(sickness.SicknessId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSickness", new { id = sickness.SicknessId }, sickness);
        }

        // DELETE: api/Sicknesses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sickness>> DeleteSickness(string id)
        {
            var sickness = await _context.Sicknesses.FindAsync(id);
            if (sickness == null)
            {
                return NotFound();
            }

            _context.Sicknesses.Remove(sickness);
            await _context.SaveChangesAsync();

            return sickness;
        }

        private bool SicknessExists(int id)
        {
            return _context.Sicknesses.Any(e => e.SicknessId == id);
        }
    }
}
