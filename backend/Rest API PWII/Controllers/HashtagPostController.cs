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
    public class HashtagPostController : ControllerBase
    {
        private PosThisDbContext db;
        private HashtagPostCore hashtagPostCore;
        public HashtagPostController(PosThisDbContext db)
        {
            this.db = db;
        }
        // GET: api/<HashtagPostController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                hashtagPostCore = new HashtagPostCore(db);
                List<HashtagPost> hashtagPost = hashtagPostCore.Get();
                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = hashtagPost,
                        Message = "Hashtagpost retrieve susccessful"
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code = 3,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }
        // POST: api/<HashtagPostController>
        [HttpPost]
        public IActionResult Create([FromBody] HashtagPost hashtagPost)
        {
            try
            {
                hashtagPostCore = new HashtagPostCore(db);
                ResponseApiError responseApiError = hashtagPostCore.Create(hashtagPost);
                if (responseApiError != null)
                {
                    return StatusCode(responseApiError.HttpStatusCode, responseApiError);
                }
                return Ok(new ResponseApiSuccess { Code = 1, Message = "HashtagPost added" });
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new ResponseApiError { Code = 1001, Message = ex.Message });
            }
        }
    }
}
