using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rest_API_PWII.Classes
{
    public class MediaPostCore
    {
        private PosThisDbContext db;
        public MediaPostCore(PosThisDbContext db)
        {
            this.db = db;
        }
        public ResponseApiError Create(MediaPost mediaPost)
        {
            try
            {
                ResponseApiError responseApiError = Validate(mediaPost);

                if (responseApiError != null)
                {
                    return responseApiError;
                }

                db.Add(mediaPost);
                db.SaveChanges();
                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Internal server error"
                };
            }
        }
        public ResponseApiError Validate(MediaPost mediaPost)
        {
            try
            {
                if (mediaPost.MediaID == null || mediaPost.PostID == null)
                {
                    return new ResponseApiError
                    {
                        Code = 2,
                        Message = "MediaPost added",
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
        public List<MediaPost> Get()
        {
            try
            {
                List<MediaPost> mediaPost = (from mp in db.MediaPosts select mp).ToList();
                return mediaPost;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
