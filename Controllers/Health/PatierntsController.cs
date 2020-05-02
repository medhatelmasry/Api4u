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
    public class PatientsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public PatientsController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            return await _context.Patients
            .Include(c => c.Sicknesses)
            .ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _context.Patients
            .Include(c => c.Sicknesses)
            .FirstOrDefaultAsync(i => i.PatientId == id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patient)
        {
            if (string.IsNullOrEmpty(patient.LastName)
                || string.IsNullOrEmpty(patient.FirstName)
                || string.IsNullOrEmpty(patient.Street)
                || string.IsNullOrEmpty(patient.City)
                || string.IsNullOrEmpty(patient.Province)
                || string.IsNullOrEmpty(patient.Country)
            ) return BadRequest("LastName, FirstName, Street, City, Province and Country are required.");

            if (id != patient.PatientId)
            {
                return BadRequest();
            }

            patient.LastName = System.Net.WebUtility.HtmlEncode(patient.LastName);
            patient.FirstName = System.Net.WebUtility.HtmlEncode(patient.FirstName);
            patient.Street = System.Net.WebUtility.HtmlEncode(patient.Street);
            patient.City = System.Net.WebUtility.HtmlEncode(patient.City);
            patient.Province = System.Net.WebUtility.HtmlEncode(patient.Province);
            patient.Country = System.Net.WebUtility.HtmlEncode(patient.Country);

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            if (string.IsNullOrEmpty(patient.LastName)
                || string.IsNullOrEmpty(patient.FirstName)
                || string.IsNullOrEmpty(patient.Street)
                || string.IsNullOrEmpty(patient.City)
                || string.IsNullOrEmpty(patient.Province)
                || string.IsNullOrEmpty(patient.Country)
            ) return BadRequest("LastName, FirstName, Street, City, Province and Country are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Patients.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            patient.LastName = System.Net.WebUtility.HtmlEncode(patient.LastName);
            patient.FirstName = System.Net.WebUtility.HtmlEncode(patient.FirstName);
            patient.Street = System.Net.WebUtility.HtmlEncode(patient.Street);
            patient.City = System.Net.WebUtility.HtmlEncode(patient.City);
            patient.Province = System.Net.WebUtility.HtmlEncode(patient.Province);
            patient.Country = System.Net.WebUtility.HtmlEncode(patient.Country);

            _context.Patients.Add(patient);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.PatientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPatient", new { id = patient.PatientId }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(string id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}
