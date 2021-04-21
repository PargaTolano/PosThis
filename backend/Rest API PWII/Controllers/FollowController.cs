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

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private PosThisDbContext db;

        public FollowController(PosThisDbContext db)
        {
            this.db = db;
        }

        [HttpGet("{id}")]
        public IActionResult GetFollowers( string id ) {
            try
            {
                var followCore = new FollowCore( db );
                var followers = followCore.GetFollowers( id );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = followers,
                        Message = "Seguidores Obtenidos Exitosamente"
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

        [HttpGet("{id}")]
        public IActionResult GetFollowing( string id )
        {
            try
            {
                var followCore = new FollowCore(db);
                var following = followCore.GetFollowing(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = following,
                        Message = "Seguidores Obtenidos Exitosamente"
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
        public IActionResult GetFollowersCount( string id )
        {
            try
            {
                var followCore = new FollowCore(db);
                var count = followCore.GetFollowingCount(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = count,
                        Message = "Seguidores Obtenidos Exitosamente"
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
        public IActionResult GetFollowingCount( string id )
        {
            try
            {
                var followCore = new FollowCore(db);
                var count = followCore.GetFollowingCount(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = count,
                        Message = "Seguidores Obtenidos Exitosamente"
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
        public IActionResult Create( FollowViewModel model )
        {
            try
            {
                var followCore = new FollowCore( db );
                var err = followCore.Create( model );
                if (err != null)
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Follow creado exitosamente"
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

        [HttpDelete("{id}")]
        public IActionResult Delete( int id )
        {
            try
            {
                var followCore = new FollowCore(db);
                var err = followCore.Delete( id );
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Follow creado exitosamente"
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
