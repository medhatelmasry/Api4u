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

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class VehiclesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public VehiclesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles
                .ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(string id)
        {
            var vehicle = await _context.Vehicles
                .FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(string id, Vehicle vehicle)
        {
            if (string.IsNullOrEmpty(vehicle.Model)
                || string.IsNullOrEmpty(vehicle.Fuel)
                || string.IsNullOrEmpty(vehicle.Type)
                || string.IsNullOrEmpty(vehicle.VehicleManufacturerName)
            ) return BadRequest("Model, Fuel, Type and VehicleManufacturerName are required.");

            if (id != vehicle.Model)
            {
                return BadRequest();
            }

            vehicle.Model = System.Net.WebUtility.HtmlEncode(vehicle.Model);
            vehicle.Fuel = System.Net.WebUtility.HtmlEncode(vehicle.Fuel);
            vehicle.Type = System.Net.WebUtility.HtmlEncode(vehicle.Type);
            vehicle.VehicleManufacturerName = System.Net.WebUtility.HtmlEncode(vehicle.VehicleManufacturerName);

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            if (string.IsNullOrEmpty(vehicle.Model)
                || string.IsNullOrEmpty(vehicle.Fuel)
                || string.IsNullOrEmpty(vehicle.Type)
                || string.IsNullOrEmpty(vehicle.VehicleManufacturerName)
            ) return BadRequest("Model, Fuel, Type and VehicleManufacturerName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Vehicles.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            vehicle.Model = System.Net.WebUtility.HtmlEncode(vehicle.Model);
            vehicle.Fuel = System.Net.WebUtility.HtmlEncode(vehicle.Fuel);
            vehicle.Type = System.Net.WebUtility.HtmlEncode(vehicle.Type);
            vehicle.VehicleManufacturerName = System.Net.WebUtility.HtmlEncode(vehicle.VehicleManufacturerName);

            _context.Vehicles.Add(vehicle);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VehicleExists(vehicle.Model))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVehicle", new { id = vehicle.Model }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(string id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        private bool VehicleExists(string id)
        {
            return _context.Vehicles.Any(e => e.Model == id);
        }
    }
}
