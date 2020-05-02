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
    public class HospitalsController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public HospitalsController(IConfiguration configuration,ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Hospitals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospitals()
        {
            return await _context.Hospitals
            .Include(c => c.Patients)
            .ToListAsync();
        }

        // GET: api/Hospitals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            var hospital = await _context.Hospitals
            .Include(c => c.Patients)
            .FirstOrDefaultAsync(i => i.HospitalId == id);

            if (hospital == null)
            {
                return NotFound();
            }

            return hospital;
        }

        // PUT: api/Hospitals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(int id, Hospital hospital)
        {
            if (string.IsNullOrEmpty(hospital.Name)
                || string.IsNullOrEmpty(hospital.Street)
                || string.IsNullOrEmpty(hospital.City)
                || string.IsNullOrEmpty(hospital.Province)
                || string.IsNullOrEmpty(hospital.Country)

            ) return BadRequest("Name, Street, City, Province, and Country are required.");

            if (id != hospital.HospitalId)
            {
                return BadRequest();
            }

            hospital.Name = System.Net.WebUtility.HtmlEncode(hospital.Name);
            hospital.Street = System.Net.WebUtility.HtmlEncode(hospital.Street);
            hospital.City = System.Net.WebUtility.HtmlEncode(hospital.City);
            hospital.Province = System.Net.WebUtility.HtmlEncode(hospital.Province);
            hospital.PostalCode = System.Net.WebUtility.HtmlEncode(hospital.PostalCode);
            hospital.Country = System.Net.WebUtility.HtmlEncode(hospital.Country);

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HospitalExists(id))
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

        // POST: api/Hospitals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital(Hospital hospital)
        {
            if (string.IsNullOrEmpty(hospital.Name)
                || string.IsNullOrEmpty(hospital.Street)
                || string.IsNullOrEmpty(hospital.City)
                || string.IsNullOrEmpty(hospital.Province)
                || string.IsNullOrEmpty(hospital.Country)

            ) return BadRequest("Name, Street, City, Province and Country are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Hospitals.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            hospital.Name = System.Net.WebUtility.HtmlEncode(hospital.Name);
            hospital.Street = System.Net.WebUtility.HtmlEncode(hospital.Street);
            hospital.City = System.Net.WebUtility.HtmlEncode(hospital.City);
            hospital.Province = System.Net.WebUtility.HtmlEncode(hospital.Province);
            hospital.PostalCode = System.Net.WebUtility.HtmlEncode(hospital.PostalCode);
            hospital.Country = System.Net.WebUtility.HtmlEncode(hospital.Country);

            _context.Hospitals.Add(hospital);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HospitalExists(hospital.HospitalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHospital", new { id = hospital.HospitalId }, hospital);
        }

        // DELETE: api/Hospitals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hospital>> DeleteHospital(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();

            return hospital;
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.HospitalId == id);
        }
    }
}
