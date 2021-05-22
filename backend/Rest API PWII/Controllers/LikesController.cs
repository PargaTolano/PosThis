using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private IHostingEnvironment env;

        public  LikesController( IHostingEnvironment env, PosThisDbContext db)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet]
        public  IActionResult Get()
        {
            try
            {
                var likesCore = new LikesCore( db, env, Request );
                var likes = likesCore.GetLikes();
                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = likes,
                        Message = "Likes retrieve successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError 
                    { 
                        Code = 500,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message 
                    });
            }
        }

        [HttpGet("{id}")]
        public  IActionResult Get( int id )
        {
            try
            {
                var likesCore = new LikesCore( db, env, Request );
                var likes = likesCore.GetPostLikes( id );

                if ( likes == null )
                    return StatusCode(
                    (int)HttpStatusCode.NotFound,
                    new ResponseApiError
                    {
                        Code = 500,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = "This post does not have likes"
                    });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = likes,
                        Message = "Post's likes retrieve successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code = 500,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }

        [HttpPost]
        [Authorize]
        public  IActionResult Create( [FromBody]LikeViewModel likes )
        {
            try 
            {
                var feedPostModel = new FeedPostModel();
                var likesCore = new LikesCore( db, env, Request);
                var err = likesCore.Create( likes, ref feedPostModel);

                if( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess 
                    { 
                        Code    = (int) HttpStatusCode.OK,
                        Data    = feedPostModel,
                        Message = "Like create successful"
                    });
            }
            catch( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError 
                    { 
                        Code = (int)HttpStatusCode.InternalServerError,
                        HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
    
        }

        [HttpDelete]
        [Authorize]
        public  IActionResult Delete( [FromBody] LikeViewModel likes )
        {
            try
            {
                var feedPostModel = new FeedPostModel();
                var likesCore = new LikesCore( db, env, Request );
                var responseApiError = likesCore.Delete( likes, ref feedPostModel );

                if ( responseApiError != null )
                {
                    return StatusCode( responseApiError.HttpStatusCode, responseApiError );
                }

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code    = (int)HttpStatusCode.OK,
                        Data    = feedPostModel,
                        Message = "Dislike successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code            = ( int ) HttpStatusCode.InternalServerError,
                        HttpStatusCode  = ( int ) HttpStatusCode.InternalServerError,
                        Message         = ex.Message
                    });
            }
        }
    }
}
