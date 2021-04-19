using Microsoft.AspNetCore.Mvc;
using Rest_API_PWII.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private PosThisDbContext db;
        public LikesController(PosThisDbContext db)
        {
            this.db = db;
        }

        // GET: api/<LikesController1>
        [HttpGet]
        public IEnumerable<Likes> Get()
        {
            List<Likes> likes = db.Likes.ToList();
            return likes;
        }

       
    }
}
