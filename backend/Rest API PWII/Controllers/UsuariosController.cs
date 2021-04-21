﻿using System;
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
                        Code = 200,
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
                var usuarioCore = new UsuarioCore( db );
                var usuario     = usuarioCore.GetOne( id );
                if ( usuario == null )
                    return StatusCode(
                        (int)HttpStatusCode.InternalServerError,
                        new ResponseApiError
                        {
                            Code = (int)HttpStatusCode.NotFound,
                            HttpStatusCode = (int)HttpStatusCode.NotFound,
                            Message = "Usuario no encontrado"
                        });

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
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
                       Code = (int)HttpStatusCode.InternalServerError,
                       HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                       Message = ex.Message
                   });
            }
           
        }

        [HttpPut("{id}")]
        public IActionResult Update( string id, [FromBody] UserViewModel usuario )
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
                var usuarioCore = new UsuarioCore(db);
                var err = usuarioCore.Delete( id );

                if ( err != null )
                    return StatusCode(err.HttpStatusCode, err);

                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 200,
                        Data = "Success",
                        Message = "Usuario borrado exitosamente"
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
