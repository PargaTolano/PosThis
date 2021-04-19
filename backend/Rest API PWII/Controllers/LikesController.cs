using Microsoft.AspNetCore.Mvc;
using Rest_API_PWII.Classes;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private PosThisDbContext db;
        private LikesCore likesCore;
        public LikesController(PosThisDbContext db)
        {
            this.db = db;
        }

        // GET: api/<LikesController>
        [HttpGet]
        public IActionResult GetLikes()
        {
            likesCore = new LikesCore(db);
            List<Likes> likes = likesCore.GetLikes();
            return Ok(likes);
        }
        [HttpPost]
        public IActionResult Create([FromBody]Likes likes)
        {
            try 
            {        
                likesCore = new LikesCore(db);
                ResponseApiError responseApiError = likesCore.Create(likes);

                if(responseApiError != null)
                {
                    return StatusCode(responseApiError.HttpStatusCode,responseApiError);
                }
                return Ok(new ResponseApiSuccess { Code = 1, Message = "Estudiante creado exitoxamente"});
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ResponseApiError { Code=1001, Message = ex.Message});
            }
    
        }



    }
}
