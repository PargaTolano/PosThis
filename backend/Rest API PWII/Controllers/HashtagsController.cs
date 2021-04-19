using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using Rest_API_PWII.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HashtagsController : ControllerBase
    {
        private PosThisDbContext db;

        public HashtagsController(PosThisDbContext db)
        {
            this.db = db;
        }

        // GET: api/<HashtagsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                HashtagCore hashtagCore = new HashtagCore(db);
                List<Hashtag> hashtag = hashtagCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = hashtag,
                        Message = "Hashtags Obtenidos Exitosamente"
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

        // POST api/<HashtagsController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Hashtag hashtag)
        {
            try
            {
                HashtagCore hashtagCore = new HashtagCore(db);
                ResponseApiError err = hashtagCore.Create(hashtag);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = hashtag,
                        Message = "Hashtag creado exitosamente"
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
    }
}
