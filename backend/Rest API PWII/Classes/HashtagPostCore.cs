using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rest_API_PWII.Classes
{
    public class HashtagPostCore
    {
        private PosThisDbContext db;
        public HashtagPostCore(PosThisDbContext db)
        {
            this.db = db;
        }
        public ResponseApiError Create(HashtagPost hashtagPost)
        {
            try
            {
                ResponseApiError responseApiError = Validate(hashtagPost);

                if (responseApiError != null)
                {
                    return responseApiError;
                }

                db.Add(hashtagPost);
                db.SaveChanges();
                return null;
            }
            catch(Exception ex)
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno del servidor"
                };
            }
        }
        public ResponseApiError Validate(HashtagPost hashtagPost)
        {
            try
            {
                if (hashtagPost.HashtagID == null || hashtagPost.PostID == null)
                {
                    return new ResponseApiError
                    {
                        Code = 2,
                        Message = "HashtagPost agregado",
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
        public List<HashtagPost> Get()
        {
            try
            {
                List<HashtagPost> hashtagPost = (from h in db.HashtagPosts select h).ToList();
                return hashtagPost;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
