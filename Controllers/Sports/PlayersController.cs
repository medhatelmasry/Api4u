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
    public class PlayersController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public PlayersController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (string.IsNullOrEmpty(player.Position)
                || string.IsNullOrEmpty(player.FirstName)
                || string.IsNullOrEmpty(player.LastName)
                || string.IsNullOrEmpty(player.TeamName)
            ) return BadRequest("Position, TeamName, FirstName and LastName are required.");

            if (id != player.PlayerId)
            {
                return BadRequest();
            }

            player.Position = System.Net.WebUtility.HtmlEncode(player.Position);
            player.FirstName = System.Net.WebUtility.HtmlEncode(player.FirstName);
            player.LastName = System.Net.WebUtility.HtmlEncode(player.LastName);
            player.TeamName = System.Net.WebUtility.HtmlEncode(player.TeamName);

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            if (string.IsNullOrEmpty(player.Position)
                || string.IsNullOrEmpty(player.FirstName)
                || string.IsNullOrEmpty(player.LastName)
                || string.IsNullOrEmpty(player.TeamName)
            ) return BadRequest("Position, TeamName, FirstName and LastName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Players.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            player.Position = System.Net.WebUtility.HtmlEncode(player.Position);
            player.FirstName = System.Net.WebUtility.HtmlEncode(player.FirstName);
            player.LastName = System.Net.WebUtility.HtmlEncode(player.LastName);
            player.TeamName = System.Net.WebUtility.HtmlEncode(player.TeamName);

            _context.Players.Add(player);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.PlayerId }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
