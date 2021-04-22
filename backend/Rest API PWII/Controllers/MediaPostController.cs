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
    public class MediaPostController : ControllerBase
    {
        private PosThisDbContext db;
        private MediaPostCore mediaPostCore;
        public MediaPostController(PosThisDbContext db)
        {
            this.db = db;
        }
        // GET: api/<MediaPost>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                mediaPostCore = new MediaPostCore(db);
                List<MediaPost> mediaPosts = mediaPostCore.Get();
                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = mediaPosts,
                        Message = "MediaPost retrieve successful"
                    });
            }
            catch (Exception ex)
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
        [HttpPost]
        public IActionResult Create([FromBody] MediaPost mediaPost)
        {
            try
            {
                mediaPostCore = new MediaPostCore(db);
                ResponseApiError responseApiError = mediaPostCore.Create(mediaPost);

                if (responseApiError != null)
                {
                    return StatusCode(responseApiError.HttpStatusCode, responseApiError);
                }
                return Ok(new ResponseApiSuccess { Code = 1, Message = "Mediapost create successful" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ResponseApiError { Code = 1001, Message = ex.Message });
            }

        }
    }
}
