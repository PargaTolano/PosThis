using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment env;

        public RepostsController(IHostingEnvironment env, PosThisDbContext db)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var repostsCore = new RepostsCore(db, env, Request);
                var reposts = repostsCore.Get();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int) HttpStatusCode.OK,
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

        [HttpPost]
        [Authorize]
        public IActionResult Create( [FromBody] RepostViewModel model )
        {
            try
            {
                var feedPostModel = new FeedPostModel();
                var repostsCore = new RepostsCore( db, env, Request);
                var err = repostsCore.Create( model, ref feedPostModel);
                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess 
                    { 
                        Code    = ( int ) HttpStatusCode.OK,
                        Data    = feedPostModel,
                        Message = "Repost create successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                   ( int ) HttpStatusCode.InternalServerError,
                   new ResponseApiError
                   {
                       Code             = ( int ) HttpStatusCode.InternalServerError,
                       HttpStatusCode   = ( int ) HttpStatusCode.InternalServerError,
                       Message          = ex.Message
                   });
            }

        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete( [FromBody] RepostViewModel id )
        {
            try
            {
                var feedPostModel   = new FeedPostModel();
                var repostsCore     = new RepostsCore( db, env, Request);
                var err = repostsCore.Delete( id, ref feedPostModel);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess 
                    { 
                        Code    = (int) HttpStatusCode.OK, 
                        Data    = feedPostModel,
                        Message = "Repost deletion successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                   ( int ) HttpStatusCode.InternalServerError,
                   new ResponseApiError
                   {
                       Code             = ( int ) HttpStatusCode.InternalServerError,
                       HttpStatusCode   = ( int ) HttpStatusCode.InternalServerError,
                       Message          = ex.Message
                   });
            }
        }
    }
}
