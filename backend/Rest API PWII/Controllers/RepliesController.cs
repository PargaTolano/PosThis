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
    public class RepliesController : ControllerBase
    {
        private PosThisDbContext db;
        private readonly IHostingEnvironment env;

        public RepliesController(IHostingEnvironment env, PosThisDbContext db)
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
                var replyCore = new RepliesCore( db, env, Request );
                var replies = replyCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Data = replies,
                        Message = "Replies retrieve successful"
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
        [Authorize]
        public IActionResult Get(int id)
        {
            try
            {
                var replyCore = new RepliesCore(db, env, Request);
                var reply   = replyCore.GetOne(id);
                if ( reply == null )
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "Reply not found"

                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data= reply,
                        Message = "Reply retrieve successful"
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

                   }) ;
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create([FromForm] CReplyModel model)
        {
            try
            {
                var repliesCore = new RepliesCore(db, env, Request);
                var err = repliesCore.Create(model);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = "Reply create successful"
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code= (int)HttpStatusCode.InternalServerError,
                        HttpStatusCode = (int) HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromForm] UReplyModel model)
        {
            try
            {
                var replyViewModel = new ReplyViewModel();
                var repliesCore = new RepliesCore( db, env, Request );
                var err = repliesCore.Update( id, model, ref replyViewModel);
                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = "Reply update successful",
                        Data = replyViewModel
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
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                RepliesCore repliesCore = new RepliesCore(db, env, Request);
                ResponseApiError err = repliesCore.Delete(id);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = (int)HttpStatusCode.OK,
                        Message = "Reply delete successful"
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
