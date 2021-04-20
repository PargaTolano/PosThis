using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rest_API_PWII.Classes;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using Rest_API_PWII.Classes;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rest_API_PWII.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private PosThisDbContext db;

        public PostController( PosThisDbContext db )
        {
            this.db = db;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var postCore = new PostCore(db);
                var posts = postCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = posts,
                        Message = "Posts Obtenidos Exitosamente"
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

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {

                var postCore = new PostCore(db);
                var post = postCore.GetOne( id );

                if (post == null)
                    return StatusCode((int)HttpStatusCode.NotFound,
                        new ResponseApiError 
                        { 
                            Code = (int ) HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "No existe el post buscado"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = post,
                        Message = "Posts Obtenidos Exitosamente"
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

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Create([FromBody] CUPostModel post)
        {
            try
            {
                var postCore = new PostCore(db);
                var err = postCore.Create( post );
                if (err != null)
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = post,
                        Message = "Post creado exitosamente"
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

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CUPostModel post)
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
                        Message = "Post actualizado exitosamente"
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

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
                        Message = "Usuario creado exitosamente"
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
