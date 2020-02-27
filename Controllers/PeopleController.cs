

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api4u.Data;
using Api4u.Models.Toons;
using Api4u.Models.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class PeopleController : ControllerBase
    {
        private readonly ToonsContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;


        public PeopleController(IConfiguration configuration,
            ToonsContext context,
            IWebHostEnvironment env
        )
        {
            _configuration = configuration;
            _context = context;
            _env = env;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<People>>> GetPeople()
        {
            var people = await _context.People.ToListAsync();

            foreach (var p in people)
            {
                if (!string.IsNullOrEmpty(p.PictureUrl))
                {
                    var pos = p.PictureUrl.ToLower().IndexOf("/images");
                    var dbhost = p.PictureUrl.Substring(0, pos);
                    p.PictureUrl = p.PictureUrl.Replace(dbhost, Helpers.GetHostUrl(Request));
                }
            }

            return people;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<People>> GetPeople(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(person.PictureUrl))
            {
                var pos = person.PictureUrl.ToLower().IndexOf("/images");
                var dbhost = person.PictureUrl.Substring(0, pos);
                person.PictureUrl = person.PictureUrl.Replace(dbhost, Helpers.GetHostUrl(Request));
            }
            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeople(int id, People people)
        {
            if (string.IsNullOrEmpty(people.FirstName)
                || string.IsNullOrEmpty(people.LastName)
                || string.IsNullOrEmpty(people.Occupation)
                || string.IsNullOrEmpty(people.Gender)
                || string.IsNullOrEmpty(people.PictureUrl)
            ) return BadRequest("FirstName, LastName, Occupation, Gender and PictureUrl are required.");

            if (id != people.Id)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(people.PictureUrl))
            {
                if (!Helpers.IsPictureInLegitToonList(people.PictureUrl, _env, Request))
                {
                    return BadRequest("Picture must be among those at /api/pictures");
                }
            }

            people.FirstName = WebUtility.HtmlEncode(people.FirstName);
            people.LastName = WebUtility.HtmlEncode(people.LastName);
            people.Occupation = WebUtility.HtmlEncode(people.Occupation);
            people.Gender = WebUtility.HtmlEncode(people.Gender);
            people.PictureUrl = WebUtility.HtmlEncode(people.PictureUrl);

            _context.Entry(people).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
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

        // POST: api/People
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<People>> PostPeople(People people)
        {
            string strMaxTblSize = _configuration["MaxTableSize"];

            if (!string.IsNullOrEmpty(strMaxTblSize) && _context.People.Count() > Convert.ToInt32(strMaxTblSize))
            {
                return BadRequest($"Number of records exceeded {strMaxTblSize}.");
            }

            if (string.IsNullOrEmpty(people.FirstName)
                || string.IsNullOrEmpty(people.LastName)
                || string.IsNullOrEmpty(people.Occupation)
                || string.IsNullOrEmpty(people.Gender)
                || string.IsNullOrEmpty(people.PictureUrl)
                ) return BadRequest("FirstName, LastName, Occupation, Gender and PictureUrl are required.");

            if (!string.IsNullOrEmpty(people.PictureUrl))
            {
                if (!Helpers.IsPictureInLegitToonList(people.PictureUrl, _env, Request))
                {
                    return BadRequest("Picture must be among those at /api/pictures");
                }
            }

            people.FirstName = WebUtility.HtmlEncode(people.FirstName);
            people.LastName = WebUtility.HtmlEncode(people.LastName);
            people.Occupation = WebUtility.HtmlEncode(people.Occupation);
            people.Gender = WebUtility.HtmlEncode(people.Gender);
            people.PictureUrl = WebUtility.HtmlEncode(people.PictureUrl);

            _context.People.Add(people);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return CreatedAtAction("GetPeople", new { id = people.Id }, people);
        }

        [HttpPost("{id:int}/vote")]
        public async Task<ActionResult<People>> IncrementVote(int id)
        {
            var people = await _context.People.FindAsync(id);

            if (people == null)
            {
                return NotFound();
            }
            people.Votes = ++people.Votes;
            //_context.Entry(people).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return people;
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<People>> DeletePeople(int id)
        {
            var people = await _context.People.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            _context.People.Remove(people);
            await _context.SaveChangesAsync();

            return people;
        }

        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }

}