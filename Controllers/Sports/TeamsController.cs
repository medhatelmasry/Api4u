using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Sports;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Sports
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class TeamsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public TeamsController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            return await _context.Teams
            .Include(t => t.Players)
            .ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(string id)
        {
            var team = await _context.Teams
            .Include(t => t.Players)
            .FirstOrDefaultAsync(i => i.TeamName == id); ;

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(string id, Team team)
        {
            if (string.IsNullOrEmpty(team.TeamName)
                || string.IsNullOrEmpty(team.City)
                || string.IsNullOrEmpty(team.Province)
                || string.IsNullOrEmpty(team.Country)
            ) return BadRequest("TeamName, City, Province and Country are required.");

            if (id != team.TeamName)
            {
                return BadRequest();
            }

            team.Country = System.Net.WebUtility.HtmlEncode(team.Country);
            team.City = System.Net.WebUtility.HtmlEncode(team.City);
            team.TeamName = System.Net.WebUtility.HtmlEncode(team.TeamName);
            team.Province = System.Net.WebUtility.HtmlEncode(team.Province);

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            if (string.IsNullOrEmpty(team.TeamName)
                || string.IsNullOrEmpty(team.City)
                || string.IsNullOrEmpty(team.Province)
                || string.IsNullOrEmpty(team.Country)
            ) return BadRequest("TeamName, City, Province and Country are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Teams.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            team.Country = System.Net.WebUtility.HtmlEncode(team.Country);
            team.City = System.Net.WebUtility.HtmlEncode(team.City);
            team.TeamName = System.Net.WebUtility.HtmlEncode(team.TeamName);
            team.Province = System.Net.WebUtility.HtmlEncode(team.Province);

            _context.Teams.Add(team);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeamExists(team.TeamName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTeam", new { id = team.TeamName }, team);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(string id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return team;
        }

        private bool TeamExists(string id)
        {
            return _context.Teams.Any(e => e.TeamName == id);
        }
    }
}
