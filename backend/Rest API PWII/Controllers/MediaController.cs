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
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {

        private PosThisDbContext db;

        public MediaController(PosThisDbContext db)
        {
            this.db = db;
        }
        // GET: api/<MediaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                MediaCore mediaCore = new MediaCore(db);
                List<Media> medias = mediaCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = medias,
                        Message = "Imagenes obtenidas exitosamente"
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

        // GET api/<MediaController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                MediaCore mediaCore = new MediaCore(db);
                Media media = mediaCore.GetOne(id);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = media,
                        Message = "Imagen Obtenida exitosamente"
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

        // POST api/<MediaController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Media media)
        {
            try
            {
                MediaCore mediaCore = new MediaCore(db);
                ResponseApiError err = mediaCore.Create(media);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = media,
                        Message = "Imagen creada exitosamente"
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

        // PUT api/<MediaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<MediaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                MediaCore mediaCore = new MediaCore(db);
                ResponseApiError err = mediaCore.Delete(id);
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = "Success",
                        Message = "Media eliminada exitosamente"
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
