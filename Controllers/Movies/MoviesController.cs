using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Movies;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class MoviesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public MoviesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movies
            .Include(m => m.Actors)
            .ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies
            .Include(a => a.Actors)
            .FirstOrDefaultAsync(i => i.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Name)
                || string.IsNullOrEmpty(movie.DirectorFirstName)
                || string.IsNullOrEmpty(movie.DirectorLastName)
            ) return BadRequest("Name, DirectorFirstName and DirectorLastName are required.");

            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            movie.Name = System.Net.WebUtility.HtmlEncode(movie.Name);
            movie.DirectorLastName = System.Net.WebUtility.HtmlEncode(movie.DirectorLastName);
            movie.DirectorLastName = System.Net.WebUtility.HtmlEncode(movie.DirectorLastName);

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            if (string.IsNullOrEmpty(movie.Name)
                || string.IsNullOrEmpty(movie.DirectorFirstName)
                || string.IsNullOrEmpty(movie.DirectorLastName)
            ) return BadRequest("Name, DirectorFirstName and DirectorLastName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Movies.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            movie.Name = System.Net.WebUtility.HtmlEncode(movie.Name);
            movie.DirectorLastName = System.Net.WebUtility.HtmlEncode(movie.DirectorLastName);
            movie.DirectorLastName = System.Net.WebUtility.HtmlEncode(movie.DirectorLastName);

            _context.Movies.Add(movie);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return movie;
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
