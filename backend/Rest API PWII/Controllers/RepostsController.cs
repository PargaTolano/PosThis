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
    public class RepostsController : ControllerBase
    {
        private PosThisDbContext db;
        private RepostsCore repostsCore;
        public RepostsController(PosThisDbContext db)
        {
            this.db = db;
        }
        // GET: api/<RepostsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                repostsCore = new RepostsCore(db);
                List<Reposts> reposts = repostsCore.Get();
                return Ok(
                    new ResponseApiSuccess
                    {
                        Code = 1,
                        Data = reposts,
                        Message = "Reposts obtenidos"
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
        [HttpPost]
        public IActionResult Create([FromBody] Reposts reposts)
        {
            try
            {
                repostsCore = new RepostsCore(db);
                ResponseApiError responseApiError = repostsCore.Create(reposts);

                if (responseApiError != null)
                {
                    return StatusCode(responseApiError.HttpStatusCode, responseApiError);
                }
                return Ok(new ResponseApiSuccess { Code = 1, Message = "Reposts agregado" });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    new ResponseApiError { Code = 1001, Message = ex.Message });
            }

        }
    }
}
