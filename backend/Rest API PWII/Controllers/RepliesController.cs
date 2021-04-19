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
    public class RepliesController : ControllerBase
    {
        private PosThisDbContext db;

        RepliesController(PosThisDbContext db)
        {
            this.db = db;
        }

        // GET: api/<RepliesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                RepliesCore replyCore = new RepliesCore(db);
                List<Reply> replies = replyCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = replies,
                        Message = "Respuestas obtenidas exitosamente"
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

        // GET api/<RepliesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                RepliesCore replyCore = new RepliesCore(db);
                Reply replies = replyCore.GetOne(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = replies,
                        Message = "Respuestas Obtenidas Exitosamente"
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

                   }) ;
            }
        }

        // POST api/<RepliesController> //CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reply reply)
        {
            try
            {
                RepliesCore repliesCore = new RepliesCore(db);
                ResponseApiError err = repliesCore.Create(reply);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = reply,
                        Message = "Respuesta creada exitosamente"
                    });
            }
            catch(Exception ex)
            {
                return StatusCode(
                    (int)HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code= 3,
                        HttpStatusCode = (int) HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }

        // PUT api/<RepliesController>/5 
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Falta

        }

        // DELETE api/<RepliesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                RepliesCore repliesCore = new RepliesCore(db);
                ResponseApiError err = repliesCore.Delete(id);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = "Success",
                        Message = "Respuesta eliminada exitosamente"
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
