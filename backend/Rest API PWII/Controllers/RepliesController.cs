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

        public RepliesController(PosThisDbContext db)
        {
            this.db = db;
        }

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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var replyCore = new RepliesCore(db);
                var reply   = replyCore.GetOne(id);
                if ( reply == null )
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "Respuesta no encontrada"

                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data= reply,
                        Message = "Respuestas Obtenidas Exitosamente"
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
        public IActionResult Create([FromBody] CUReplyModel model)
        {
            try
            {
                var repliesCore = new RepliesCore(db);
                var err = repliesCore.Create(model);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CUReplyModel model)
        {
            try
            {
                var repliesCore = new RepliesCore( db );
                var err = repliesCore.Update( id, model );
                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Message = "Respuesta editada exitosamente"
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
                        Code = (int)HttpStatusCode.OK,
                        Message = "Respuesta eliminada exitosamente"
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
