using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rest_API_PWII.Classes
{
    public class LikesCore
    {
        private PosThisDbContext db;
        public LikesCore(PosThisDbContext db)
        {
            this.db = db;
        }
        public ResponseApiError Create(Likes likes)
        {
            try
            {
                ResponseApiError responseApiError = Validate(likes);

                if(responseApiError != null)
                {
                    return responseApiError;
                }

                db.Add(likes);
                db.SaveChanges();
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public ResponseApiError Validate(Likes likes)
        {
            try
            {
                if(likes.PostID == null || likes.UsuarioID == null)
                {
                    return new ResponseApiError { Code = 2, Message = "Like agregado" ,HttpStatusCode = (int)HttpStatusCode.BadRequest};
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Likes> GetLikes()
        {
            try 
            {
                List<Likes> likes = (from l in db.Likes select l).ToList();
                return likes;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
