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
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private PosThisDbContext db;

        UsuariosController( PosThisDbContext db )
        {
            this.db = db;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                UsuarioCore usuarioCore = new UsuarioCore(db);
                List<Usuario> usuarios = usuarioCore.GetAll();

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = usuarios,
                        Message = "Usuarios Obtenidos Exitosamente"
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

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public Usuario Get( int id )
        {
            UsuarioCore usuarioCore = new UsuarioCore( db );
            return usuarioCore.GetOne( id );
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<IActionResult> Post( [FromBody] Usuario usuario )
        {
            try
            {
                UsuarioCore usuarioCore = new UsuarioCore( db );
                ResponseApiError err = usuarioCore.Create( usuario );
                if ( err == null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess {
                        Code = 1, 
                        Data = usuario,
                        Message = "Usuario creado exitosamente" 
                    } );
            }
            catch(Exception ex)
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError
                    {
                        Code = 3,
                        HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                        Message = ex.Message
                    });
            }
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public IActionResult Put( int id, [FromBody] Usuario usuario )
        {
            try
            {
                UsuarioCore usuarioCore = new UsuarioCore( db );
                ResponseApiError err = usuarioCore.Update( id, usuario );
                if ( err == null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = usuario,
                        Message = "Usuario creado exitosamente"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError {
                        Code = 3,
                        HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                        Message = ex.Message 
                    });
            }
        }

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete( int id )
        {
            return null;
        }
    }
}
