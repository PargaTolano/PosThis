using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rest_API_PWII.Classes
{
    public class RepostsCore
    {
        private PosThisDbContext db;
        public RepostsCore(PosThisDbContext db)
        {
            this.db = db;
        }
        public ResponseApiError Create(Reposts reposts)
        {
            try
            {
                ResponseApiError responseApiError = Validate(reposts);

                if (responseApiError != null)
                {
                    return responseApiError;
                }

                db.Add(reposts);
                db.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno del servidor"
                };
            }
        }
        public ResponseApiError Validate(Reposts reposts)
        {
            try
            {
                if (reposts.Texto == null || reposts.FechaPublicacion == null || reposts.PostID == null || reposts.UsuarioID == null)
                {
                    return new ResponseApiError
                    {
                        Code = 2,
                        Message = "Reposts agregado",
                        HttpStatusCode = (int)HttpStatusCode.BadRequest
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Reposts> Get()
        {
            try
            {
                List<Reposts> reposts = (from rp in db.Reposts select rp).ToList();
                return reposts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
