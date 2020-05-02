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
    public class MedicinesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public MedicinesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
            return await _context.Medicines
            .ToListAsync();
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
            var medicine = await _context.Medicines
            .FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }

        // PUT: api/Medicines/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, Medicine medicine)
        {
            if (string.IsNullOrEmpty(medicine.Name)
               || string.IsNullOrEmpty(medicine.DosageUnit)

            ) return BadRequest("Name and DosageUnit are required.");

            if (id != medicine.MedicineId)
            {
                return BadRequest();
            }

            medicine.Name = System.Net.WebUtility.HtmlEncode(medicine.Name);
            medicine.DosageUnit = System.Net.WebUtility.HtmlEncode(medicine.DosageUnit);

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
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

        // POST: api/Medicines
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
            if (string.IsNullOrEmpty(medicine.Name)
               || string.IsNullOrEmpty(medicine.DosageUnit)

            ) return BadRequest("Name and DosageUnit are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Medicines.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            medicine.Name = System.Net.WebUtility.HtmlEncode(medicine.Name);
            medicine.DosageUnit = System.Net.WebUtility.HtmlEncode(medicine.DosageUnit);

            _context.Medicines.Add(medicine);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MedicineExists(medicine.MedicineId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMedicine", new { id = medicine.MedicineId }, medicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Medicine>> DeleteMedicine(string id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();

            return medicine;
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.MedicineId == id);
        }
    }
}
