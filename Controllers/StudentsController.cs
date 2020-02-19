using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api4u.Data;
using Api4u.Models.Students;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class StudentsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public StudentsController(
            IConfiguration configuration,
            ToonsContext context
        )
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(string id, Student student)
        {
            if (string.IsNullOrEmpty(student.FirstName)
                || string.IsNullOrEmpty(student.LastName)
                || string.IsNullOrEmpty(student.School)
                || string.IsNullOrEmpty(student.StudentId)
            ) return BadRequest("FirstName, LastName, School and StudentId are required.");

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            student.StudentId = WebUtility.HtmlEncode(student.StudentId);
            student.FirstName = WebUtility.HtmlEncode(student.FirstName);
            student.LastName = WebUtility.HtmlEncode(student.LastName);
            student.School = WebUtility.HtmlEncode(student.School);

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.FirstName)
                || string.IsNullOrEmpty(student.LastName)
                || string.IsNullOrEmpty(student.School)
                || string.IsNullOrEmpty(student.StudentId)
            ) return BadRequest("FirstName, LastName, School and StudentId are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Students.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            student.StudentId = WebUtility.HtmlEncode(student.StudentId);
            student.FirstName = WebUtility.HtmlEncode(student.FirstName);
            student.LastName = WebUtility.HtmlEncode(student.LastName);
            student.School = WebUtility.HtmlEncode(student.School);



            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }

}