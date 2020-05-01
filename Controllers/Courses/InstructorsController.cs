using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Courses;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Courses
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class InstructorsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public InstructorsController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
        {
            return await _context.Instructors
            .Include(i => i.Courses)
            .ToListAsync();
        }

        // GET: api/Instructors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instructor>> GetInstructor(int id)
        {
            var instructor = await _context.Instructors
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(i => i.InstructorId == id);


            if (instructor == null)
            {
                return NotFound();
            }

            return instructor;
        }

        // PUT: api/Instructors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstructor(int id, Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.Email)
                || string.IsNullOrEmpty(instructor.FirstName)
                || string.IsNullOrEmpty(instructor.LastName)
            ) return BadRequest("Email, FirstName and LastName are required.");

            if (id != instructor.InstructorId)
            {
                return BadRequest();
            }

            instructor.Email = System.Net.WebUtility.HtmlEncode(instructor.Email);
            instructor.FirstName = System.Net.WebUtility.HtmlEncode(instructor.FirstName);
            instructor.LastName = System.Net.WebUtility.HtmlEncode(instructor.LastName);

            _context.Entry(instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        // POST: api/Instructors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Instructor>> PostInstructor(Instructor instructor)
        {
            if (string.IsNullOrEmpty(instructor.Email)
                || string.IsNullOrEmpty(instructor.FirstName)
                || string.IsNullOrEmpty(instructor.LastName)
            ) return BadRequest("Email, FirstName and LastName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Instructors.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            instructor.Email = System.Net.WebUtility.HtmlEncode(instructor.Email);
            instructor.FirstName = System.Net.WebUtility.HtmlEncode(instructor.FirstName);
            instructor.LastName = System.Net.WebUtility.HtmlEncode(instructor.LastName);

            _context.Instructors.Add(instructor);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstructor", new { id = instructor.InstructorId }, instructor);
        }

        // DELETE: api/Instructors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instructor>> DeleteInstructor(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

            return instructor;
        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.InstructorId == id);
        }
    }
}
