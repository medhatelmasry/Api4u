using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api4u.Data;
using Api4u.Models.Countries;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("ToonsPolicy")]
    public class ProvincesController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;

        public ProvincesController(IConfiguration configuration, ToonsContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: api/Provincess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvince()
        {
            return await _context.Provinces
            .Include(p => p.Cities)
            .ToListAsync();
        }

        // GET: api/Provincess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
            var province = await _context.Provinces
            .Include(p => p.Cities)
            .FirstOrDefaultAsync(i => i.ProvinceId == id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }

        // PUT: api/Provincess/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProvince(int id, Province province)
        {
            if (string.IsNullOrEmpty(province.Name)
                || string.IsNullOrEmpty(province.CapitalCity)
                || string.IsNullOrEmpty(province.CountryName)
            ) return BadRequest("Name, CapitalCity and CountryName are required.");

            if (id != province.ProvinceId)
            {
                return BadRequest();
            }

            province.Name = System.Net.WebUtility.HtmlEncode(province.Name);
            province.CapitalCity = System.Net.WebUtility.HtmlEncode(province.CapitalCity);
            province.CountryName = System.Net.WebUtility.HtmlEncode(province.CountryName);

            _context.Entry(province).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceExists(id))
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

        // POST: api/Provincess
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Province>> PostProvince(Province province)
        {
            if (string.IsNullOrEmpty(province.Name)
                || string.IsNullOrEmpty(province.CapitalCity)
                || string.IsNullOrEmpty(province.CountryName)
            ) return BadRequest("Name, CapitalCity and CountryName are required.");

            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.Provinces.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            province.Name = System.Net.WebUtility.HtmlEncode(province.Name);
            province.CapitalCity = System.Net.WebUtility.HtmlEncode(province.CapitalCity);
            province.CountryName = System.Net.WebUtility.HtmlEncode(province.CountryName);

            _context.Provinces.Add(province);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvince", new { id = province.ProvinceId }, province);
        }

        // DELETE: api/Provincess/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Province>> DeleteProvince(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();

            return province;
        }

        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.ProvinceId == id);
        }
    }
}
