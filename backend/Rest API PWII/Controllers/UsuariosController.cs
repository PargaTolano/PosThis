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
    public class UsuariosController : ControllerBase
    {
        private PosThisDbContext db;

        public UsuariosController( PosThisDbContext db )
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarioCore = new UsuarioCore( db );
                var usuarios    = usuarioCore.GetAll();

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

        [HttpGet("{id}")]
        public IActionResult Get( string id )
        {
            try
            {
                var usuarioCore = new UsuarioCore( db );
                var usuario     = usuarioCore.GetOne( id );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = usuario,
                        Message = "Usuarios Obtenidos Exitosamente"
                    });
            }
            catch ( Exception ex )
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

        [HttpPost]
        public async Task<IActionResult> Create( [FromBody] Usuario usuario )
        {
            try
            {
                var usuarioCore = new UsuarioCore( db );
                var err         = usuarioCore.Create( usuario );
                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess {
                        Code = 1, 
                        Data = usuario,
                        Message = "Usuario creado exitosamente" 
                    } );
            }
            catch( Exception ex )
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

        [HttpPut("{id}")]
        public IActionResult Update( string id, [FromBody] Usuario usuario )
        {
            try
            {
                var usuarioCore = new UsuarioCore( db );
                var err = usuarioCore.Update( id, usuario );

                if ( err != null )
                    return StatusCode( err.HttpStatusCode, err );

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = usuario,
                        Message = "Usuario editado exitosamente"
                    });
            }
            catch ( Exception ex )
            {
                return StatusCode(
                    ( int ) HttpStatusCode.InternalServerError,
                    new ResponseApiError {
                        Code = 3,
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
                var usuarioCore = new UsuarioCore(db);
                var err = usuarioCore.Delete( id );

                if ( err != null )
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = "Success",
                        Message = "Usuario creado exitosamente"
                    });
            }
            catch ( Exception ex )
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
    }
}
