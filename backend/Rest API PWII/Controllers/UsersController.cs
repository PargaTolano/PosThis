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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private PosThisDbContext db;
        private IHostingEnvironment env;

        public UsersController(IHostingEnvironment env, PosThisDbContext db)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var userCore = new UserCore(db, env, Request);
                var users = userCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = users,
                        Message = "Users retrieval successful"
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

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get( string id, [FromHeader(Name = "UserID")] string viewerId )
        {
            try
            {
                var userCore = new UserCore(db, env, Request);
                var user = userCore.GetOne(id, viewerId);
                if (user == null)
                    return StatusCode(
                        (int)HttpStatusCode.NotFound,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "User not found"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = user,
                        Message = "User retrieval successful"
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

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetFeed( string id )
        {
            try
            {
                var userCore = new UserCore(db, env, Request);
                var feed = userCore.GetFeed(id);
                if (feed == null)
                    return StatusCode(
                        (int)HttpStatusCode.NotFound,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "Feed not found"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = feed,
                        Message = "Feed retrieval successful"
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

        [HttpGet]
        [Authorize]
        public IActionResult GetSearch( [FromQuery] SearchRequestModel model ) {
            try
            {
                var userCore = new UserCore(db, env, Request);
                var searchResult = userCore.GetSearch(model);
                if (searchResult == null)
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "At least one criteria should be true"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int) HttpStatusCode.OK,
                        Data = searchResult,
                        Message = "Search request successful"
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

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetUserPosts( string id, [FromHeader(Name = "UserID")] string viewerId)
        {
            try
            {
                var userCore = new UserCore(db, env, Request);
                var searchResult = userCore.GetUserPosts( id, viewerId);
                if (searchResult == null)
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "At least one criteria should be true"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = searchResult,
                        Message = "Search request successful"
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

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update( string id, [FromForm] UpdateUserViewModel model )
        {
            try
            {
                var user = new UserViewModel();
                var userCore = new UserCore(db, env, Request);
                var err = userCore.Update(id, model, ref user);

                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = user,
                        Message = "User data update successful"
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError {
                        Code = (int)HttpStatusCode.InternalServerError,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult AddProfilePic( string id, [FromForm] IFormFile file )
        {
            try
            {
                var userCore = new UserCore( db, env, Request );
                var err = userCore.UploadProfilePic( id, file );

                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = ( int ) HttpStatusCode.OK,
                        Data = new int[]{ },
                        Message = "User data update successful"
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

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult AddCoverPic( string id, [FromForm] IFormFile file )
        {
            try
            {
                var userCore = new UserCore( db, env, Request );
                var err = userCore.UploadCoverPic( id, file );

                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = new int[] { },
                        Message = "User data update successful"
                    });
            }
            catch ( Exception ex )
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

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete( string id )
        {
            try
            {
                var userCore = new UserCore(db, env, Request);
                var err = userCore.Delete( id );

                if ( err != null )
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = "Success",
                        Message = "User deletion successful"
                    });
            }
            catch ( Exception ex )
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
    }
}
