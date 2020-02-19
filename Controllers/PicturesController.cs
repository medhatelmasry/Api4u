using System.Collections.Generic;
using Api4u.Models.Toons;
using Api4u.Models.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Api4u.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ToonsPolicy")]
    public class PicturesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public PicturesController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // GET: api/Values
        [HttpGet]
        public IEnumerable<Picture> Get()
        {
            return Helpers.GetPictures(_env, Request);
        }
    }

}