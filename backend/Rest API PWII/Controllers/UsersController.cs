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
    public class UsersController : ControllerBase
    {
        private PosThisDbContext db;

        public UsersController( PosThisDbContext db )
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var userCore = new UserCore( db );
                var users    = userCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = users,
                        Message = "Users retrieve successful"
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
        public IActionResult Get( string id )
        {
            try
            {
                var userCore = new UserCore( db );
                var user     = userCore.GetOne( id );
                if ( user == null )
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "User not found"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = user,
                        Message = "User retrieve successful"
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

        [HttpPut("{id}")]
        public IActionResult Update( string id, [FromBody] UserViewModel user )
        {
            try
            {
                var userCore = new UserCore( db );
                var err = userCore.Update( id, user );

                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = user,
                        Message = "User data update successful"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError {
                        Code = (int)HttpStatusCode.InternalServerError,
                        HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                        Message = ex.Message 
                    });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete( string id )
        {
            try
            {
                var userCore = new UserCore(db);
                var err = userCore.Delete( id );

                if ( err != null )
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = "Success",
                        Message = "User delete successful"
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
