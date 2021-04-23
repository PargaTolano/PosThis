using Microsoft.AspNetCore.Mvc;
using Rest_API_PWII.Classes;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RepostsController : ControllerBase
    {
        private PosThisDbContext db;

        public RepostsController(PosThisDbContext db)
        {
            this.db = db;
        }

        // GET: api/<RepostsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var repostsCore = new RepostsCore(db);
                var reposts = repostsCore.Get();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = reposts,
                        Message = "Reposts retrieve successful"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }
        // POST: api/<RepostController>
        [HttpPost]
        public IActionResult Create( [FromBody] CRepostModel model )
        {
            try
            {
                var repostsCore = new RepostsCore( db );
                var err = repostsCore.Create( model );
                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess 
                    { 
                        Code = 200, 
                        Message = "Repost create successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                   ( int ) HttpStatusCode.InternalServerError,
                   new ResponseApiError
                   {
                       Code = ( int ) HttpStatusCode.InternalServerError,
                       HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                       Message = ex.Message
                   });
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete( CRepostModel model )
        {
            try
            {
                var repostsCore = new RepostsCore( db );
                var err = repostsCore.Delete( model );
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess 
                    { 
                        Code = 200, 
                        Message = "Repost delete successful"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(
                   (int)HttpStatusCode.InternalServerError,
                   new ResponseApiError
                   {
                       Code = (int)HttpStatusCode.InternalServerError,
                       HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                       Message = ex.Message
                   });
            }
        }
    }
}
