using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class HashtagCore
    {
        private PosThisDbContext db;

        public HashtagCore(PosThisDbContext db)
        {
            this.db = db;
        }

        public ResponseApiError Validate(Hashtag hashtag)
        {
            if (hashtag.Texto==null || hashtag.Texto=="")
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Los datos del hashtag no son validos"
                };
            return null;
        }

        public int ValidateExists(Hashtag hashtag)
        {
            var res = (from h in db.Hashtags where h.Texto == hashtag.Texto select h).FirstOrDefault();

            if (res == null)
                return 0;

            return 1;
        }
        public ResponseApiError Create(Hashtag hashtag)
        {
            try
            {
                ResponseApiError err = Validate(hashtag);
                if (err != null)
                    return err;
                int exists = ValidateExists(hashtag);
                
                if (exists == 1)
                    return null;

                db.Hashtags.Add(hashtag);
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

        public List<Hashtag> GetAll()
        {
            List<Hashtag> hashtags = (from u in db.Hashtags select u).ToList();
            return hashtags;
        }

    }
}
