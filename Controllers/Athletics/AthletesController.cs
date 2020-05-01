using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Athletics;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Athletics
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class AthletesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public AthletesController(IConfiguration configuration,ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Athletes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Athlete>>> GetAthletes()
        {
            return await _context.Athletes
            .ToListAsync();
        }

        // GET: api/Athletes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Athlete>> GetAthlete(int id)
        {
            var athlete = await _context.Athletes
            .FindAsync(id);

            if (athlete == null)
            {
                return NotFound();
            }

            return athlete;
        }

        // PUT: api/Athletes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthlete(int id, Athlete athlete)
        {
            if (string.IsNullOrEmpty(athlete.Country)
                || string.IsNullOrEmpty(athlete.FirstName)
                || string.IsNullOrEmpty(athlete.LastName)
            ) return BadRequest("Country, FirstName and LastName are required.");

            if (id != athlete.AthleteId)
            {
                return BadRequest();
            }

            athlete.Country = System.Net.WebUtility.HtmlEncode(athlete.Country);
            athlete.FirstName = System.Net.WebUtility.HtmlEncode(athlete.FirstName);
            athlete.LastName = System.Net.WebUtility.HtmlEncode(athlete.LastName);

            _context.Entry(athlete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
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

        // POST: api/Athletes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Athlete>> PostAthlete(Athlete athlete)
        {
            if (string.IsNullOrEmpty(athlete.Country)
                || string.IsNullOrEmpty(athlete.FirstName)
                || string.IsNullOrEmpty(athlete.LastName)
            ) return BadRequest("Country, FirstName and LastName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Athletes.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }
            athlete.Country = System.Net.WebUtility.HtmlEncode(athlete.Country);
            athlete.FirstName = System.Net.WebUtility.HtmlEncode(athlete.FirstName);
            athlete.LastName = System.Net.WebUtility.HtmlEncode(athlete.LastName);

            _context.Athletes.Add(athlete);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAthlete", new { id = athlete.AthleteId }, athlete);
        }

        // DELETE: api/Athletes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Athlete>> DeleteAthlete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }

            _context.Athletes.Remove(athlete);
            await _context.SaveChangesAsync();

            return athlete;
        }

        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.AthleteId == id);
        }
    }
}
