using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class MediaController : ControllerBase
    {

        private PosThisDbContext db;
        private readonly IHostingEnvironment env;

        public MediaController(IHostingEnvironment env, PosThisDbContext db)
        {
            this.db = db;
            this.env = env;
        }

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var mediaCore = new MediaCore(db, env, Request);
        //        var medias = mediaCore.GetAll();
        //
        //        return Ok(
        //            new ResponseApiSuccess
        //            {
        //                Code = (int)HttpStatusCode.OK,
        //                Data = medias,
        //                Message = "Media retrieve successful"
        //            });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(
        //            (int)HttpStatusCode.InternalServerError,
        //            new ResponseApiError
        //            {
        //                Code = (int)HttpStatusCode.InternalServerError,
        //                HttpStatusCode = (int)HttpStatusCode.InternalServerError,
        //                Message = ex.Message
        //            });
        //    }
        //}

        //[HttpGet("{id}")]
        //public IActionResult Get( int id )
        //{
        //    try
        //    {
        //        var mediaCore = new MediaCore(db, env, Request);
        //        var media = mediaCore.GetOne( id );
        //
        //        if ( media == null ) {
        //            return StatusCode(
        //           (int)HttpStatusCode.NotFound,
        //           new ResponseApiError
        //           {
        //               Code = (int)HttpStatusCode.NotFound,
        //               HttpStatusCode = (int)HttpStatusCode.NotFound,
        //               Message = "El archivo de media solicitado no existe"
        //           });
        //        }
        //
        //        return Redirect(media.Path);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(
        //           (int)HttpStatusCode.InternalServerError,
        //           new ResponseApiError
        //           {
        //               Code = (int)HttpStatusCode.InternalServerError,
        //               HttpStatusCode = (int)HttpStatusCode.InternalServerError,
        //               Message = ex.Message
        //           });
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create( [FromForm] List<IFormFile> files )
        //{
        //    try
        //    {
        //        var list = new List<Media>();
        //        var mediaCore = new MediaCore( db, env, Request );
        //        var err = mediaCore.Create( files, ref list );
        //        if (err != null)
        //            return StatusCode(err.HttpStatusCode, err);
        //
        //        return Ok(
        //            new ResponseApiSuccess
        //            {
        //                Code = (int)HttpStatusCode.OK,
        //                Data = {},
        //                Message = "Media create successful"
        //            });
        //    }
        //    catch ( Exception ex )
        //    {
        //        return StatusCode(
        //            (int)HttpStatusCode.InternalServerError,
        //            new ResponseApiError
        //            {
        //                Code = (int)HttpStatusCode.InternalServerError,
        //                HttpStatusCode = (int)HttpStatusCode.InternalServerError,
        //                Message = ex.Message
        //            });
        //    }
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    try
        //    {
        //        var mediaCore = new MediaCore(db, env, Request);
        //        var err = mediaCore.Delete(id);
        //        if (err != null)
        //            return StatusCode( err.HttpStatusCode, err );
        //
        //        return Ok(
        //            new ResponseApiSuccess
        //            {
        //                Code = (int)HttpStatusCode.OK,
        //                Data = { },
        //                Message = "Media delete successful"
        //            });
        //    }
        //    catch ( Exception ex )
        //    {
        //        return StatusCode(
        //            (int)HttpStatusCode.InternalServerError,
        //            new ResponseApiError
        //            {
        //                Code = (int)HttpStatusCode.InternalServerError,
        //                HttpStatusCode = (int)HttpStatusCode.InternalServerError,
        //                Message = ex.Message
        //            });
        //    }
        //}
    }
}
