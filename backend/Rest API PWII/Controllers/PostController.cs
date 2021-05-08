using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API_PWII.Classes;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System.Net;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private PosThisDbContext db;
        private readonly IHostingEnvironment _env;

        public PostController( IHostingEnvironment env, PosThisDbContext db )
        {
            this.db = db;
            _env = env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var postCore = new PostCore(db);
                var posts = postCore.GetAll( Request.Scheme, Request.Host.ToString(), Request.PathBase );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = ( int )HttpStatusCode.OK,
                        Data = posts,
                        Message = "Posts retrieve successful"
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

        [HttpGet("{id}")]
        public IActionResult Get( int id )
        {
            try
            {
                var postCore = new PostCore( db );
                var post = postCore.GetOne( id, Request.Scheme, Request.Host.ToString(), Request.PathBase );

                if (post == null)
                    return StatusCode((int)HttpStatusCode.NotFound,
                        new ResponseApiError 
                        { 
                            Code = ( int ) HttpStatusCode.NotFound,
                            HttpStatusCode = ( int ) HttpStatusCode.NotFound,
                            Message = "Post not found"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = ( int ) HttpStatusCode.OK,
                        Data = post,
                        Message = "Post retrieve successful"
                    });
            }
            catch (Exception ex)
            {

                return StatusCode(
                   (int)HttpStatusCode.InternalServerError,
                   new ResponseApiError
                   {
                       Code = ( int ) HttpStatusCode.InternalServerError,
                       HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                       Message = ex.Message
                   });
            }
        }

        [HttpPost]
        public IActionResult Create( [FromForm] CPostModel post )
        {
            try
            {
                var postCore = new PostCore( db );
                var err = postCore.Create( post, Request.Scheme, Request.Host.ToString(), Request.PathBase );
                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = ( int ) HttpStatusCode.OK,
                        Data = post,
                        Message = "Post create successful"
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

        [HttpPut("{id}")]
        public IActionResult Update( int id, [FromBody] CPostModel post )
        {
            try
            {
                var postCore= new PostCore(db);
                var err = postCore.Update(id, post);

                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = post,
                        Message = "Post update successful"
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

        [HttpDelete("{id}")]
        public IActionResult Delete( int id )
        {
            try
            {
                var postCore = new PostCore(db);
                var err = postCore.Delete( id );
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Post delete successful"
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
