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

        public LikesController(PosThisDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var likesCore = new LikesCore(db);
                var likes = likesCore.GetLikes();
                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = likes,
                        Message = "Likes retrieve successful"
                    });
            }
            catch (Exception ex)
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
        public IActionResult Get( int id )
        {
            try
            {
                var likesCore = new LikesCore( db );
                var likes = likesCore.GetPostLikeCount( id );
                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = likes,
                        Message = "Likes count retrieve successful"
                    });
            }
            catch (Exception ex)
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
        public IActionResult Create( [FromBody]CULikeModel likes )
        {
            try 
            {        
                var likesCore = new LikesCore( db );
                var responseApiError = likesCore.Create( likes );

                if(responseApiError != null)
                {
                    return StatusCode( responseApiError.HttpStatusCode,responseApiError );
                }
                return Ok(
                    new ResponseApiSuccess 
                    { 
                        Code = 200,
                        Message = "Like create successful"
                    });
            }
            catch( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError 
                    { 
                        Code = 500,
                        HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
    
        }

        [HttpDelete]
        public IActionResult Delete( [FromBody] CULikeModel likes )
        {
            try
            {
                var likesCore = new LikesCore( db );
                var responseApiError = likesCore.Delete( likes );

                if ( responseApiError != null )
                {
                    return StatusCode( responseApiError.HttpStatusCode, responseApiError );
                }

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Unlike successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code = 500,
                        HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }
    }
}
