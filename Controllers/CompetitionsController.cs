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

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class CompetitionsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public CompetitionsController(IConfiguration configuration,ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Competitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
        {
            return await _context.Competitions
            .Include(c => c.Athletes)
            .ToListAsync();
        }

        // GET: api/Competitions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Competition>> GetCompetition(int id)
        {
            var competition = await _context.Competitions
            .Include(c => c.Athletes)
            .FirstOrDefaultAsync(i => i.CompetitionId == id);

            if (competition == null)
            {
                return NotFound();
            }

            return competition;
        }

        // PUT: api/Competitions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompetition(int id, Competition competition)
        {
            if (string.IsNullOrEmpty(competition.EventName)
            ) return BadRequest("EventName  required.");

            if (id != competition.CompetitionId)
            {
                return BadRequest();
            }

            competition.EventName = System.Net.WebUtility.HtmlEncode(competition.EventName);

            _context.Entry(competition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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

        // POST: api/Competitions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
        {
            if (string.IsNullOrEmpty(competition.EventName)
            ) return BadRequest("EventName  required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Competitions.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            competition.EventName = System.Net.WebUtility.HtmlEncode(competition.EventName);

            _context.Competitions.Add(competition);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetition", new { id = competition.CompetitionId }, competition);
        }

        // DELETE: api/Competitions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Competition>> DeleteCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }

            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();

            return competition;
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.CompetitionId == id);
        }
    }
}
