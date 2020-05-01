using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Vehicles;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers.Vehicles
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class VehicleManufacturersController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public VehicleManufacturersController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/VehicleManufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleManufacturer>>> GetVehicleManufacturers()
        {
            return await _context.VehicleManufacturers
            .Include(v => v.Vehicles)
            .ToListAsync();
        }

        // GET: api/VehicleManufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleManufacturer>> GetVehicleManufacturer(string id)
        {
            var vehicleManufacturer = await _context.VehicleManufacturers
            .Include(v => v.Vehicles)
            .FirstOrDefaultAsync(i => i.VehicleManufacturerName == id);

            if (vehicleManufacturer == null)
            {
                return NotFound();
            }

            return vehicleManufacturer;
        }

        // PUT: api/VehicleManufacturers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleManufacturer(string id, VehicleManufacturer vehicleManufacturer)
        {
            if (string.IsNullOrEmpty(vehicleManufacturer.VehicleManufacturerName)
                || string.IsNullOrEmpty(vehicleManufacturer.Country)
            ) return BadRequest("VehicleManufacturerName and Country are required.");

            if (id != vehicleManufacturer.VehicleManufacturerName)
            {
                return BadRequest();
            }

            vehicleManufacturer.VehicleManufacturerName = System.Net.WebUtility.HtmlEncode(vehicleManufacturer.VehicleManufacturerName);
            vehicleManufacturer.Country = System.Net.WebUtility.HtmlEncode(vehicleManufacturer.Country);

            _context.Entry(vehicleManufacturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleManufacturerExists(id))
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

        // POST: api/VehicleManufacturers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VehicleManufacturer>> PostVehicleManufacturer(VehicleManufacturer vehicleManufacturer)
        {
            if (string.IsNullOrEmpty(vehicleManufacturer.VehicleManufacturerName)
                || string.IsNullOrEmpty(vehicleManufacturer.Country)
            ) return BadRequest("VehicleManufacturerName and Country are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.VehicleManufacturers.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            vehicleManufacturer.VehicleManufacturerName = System.Net.WebUtility.HtmlEncode(vehicleManufacturer.VehicleManufacturerName);
            vehicleManufacturer.Country = System.Net.WebUtility.HtmlEncode(vehicleManufacturer.Country);

            _context.VehicleManufacturers.Add(vehicleManufacturer);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehicleManufacturerExists(vehicleManufacturer.VehicleManufacturerName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVehicleManufacturer", new { id = vehicleManufacturer.VehicleManufacturerName }, vehicleManufacturer);
        }

        // DELETE: api/VehicleManufacturers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VehicleManufacturer>> DeleteVehicleManufacturer(string id)
        {
            var vehicleManufacturer = await _context.VehicleManufacturers.FindAsync(id);
            if (vehicleManufacturer == null)
            {
                return NotFound();
            }

            _context.VehicleManufacturers.Remove(vehicleManufacturer);
            await _context.SaveChangesAsync();

            return vehicleManufacturer;
        }

        private bool VehicleManufacturerExists(string id)
        {
            return _context.VehicleManufacturers.Any(e => e.VehicleManufacturerName == id);
        }
    }
}
