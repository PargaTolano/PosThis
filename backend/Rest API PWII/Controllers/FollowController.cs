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

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private PosThisDbContext db;
        private IHostingEnvironment env;

        public FollowController(IHostingEnvironment env, PosThisDbContext db)
        {
            this.db = db;
            this.env = env;
        }

        [HttpGet("{id}")]
        public IActionResult GetFollowers( string id ) {
            try
            {
                var followCore = new FollowCore( db, env, Request );
                var followers = followCore.GetFollowers( id );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = followers,
                        Message = "Followers retrieve successful"
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
                var followCore = new FollowCore(db, env, Request);
                var following = followCore.GetFollowing(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = following,
                        Message = "Followed users retrieve successful"
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
                var followCore = new FollowCore(db, env, Request);
                var count = followCore.GetFollowersCount(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = count,
                        Message = "Follower count retrieve successful"
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
                var followCore = new FollowCore(db, env, Request);
                var count = followCore.GetFollowingCount(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = count,
                        Message = "Followed users count retrieve successful"
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
                var followCore = new FollowCore( db, env, Request);
                var err = followCore.Create( model );
                if (err != null)
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Follow successful"
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
                var followCore = new FollowCore(db, env, Request);
                var err = followCore.Delete( id );
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Unfollow successful"
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
