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
        
        private ResponseApiError Validate(CULikeModel model )
        {
            if ( model.PostID == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post de like no puede ser null",
                };

            if ( model.UserID == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Usuario de like no puede ser null",
                };

            var post = db.Posts.FirstOrDefault(p => p.PostID == model.PostID);

            if ( post == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Post dado para el like no existe",
                };

            var usuario = db.Users.FirstOrDefault(u => u.Id == model.UserID );

            if (usuario == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Post dado para el like no existe",
                };

            var like = db.Likes.FirstOrDefault(l =>
                   l.PostID == model.PostID &&
                   l.UserID == model.UserID
                );

            if (like != null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Like en el post ya existente",
                };

            return null;
        }

        private ResponseApiError ValidateCU( CULikeModel model )
        {
            if ( model.PostID == null )
                return new ResponseApiError 
                { 
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "PostID de like no puede ser null", 
                    HttpStatusCode = (int)HttpStatusCode.BadRequest 
                };

            if ( model.UserID == null )  
                return new ResponseApiError 
                { 
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "UsuarioID de like no puede ser null", 
                    HttpStatusCode = (int)HttpStatusCode.BadRequest 
                };

            return null;
        }

        public ResponseApiError Create( CULikeModel model )
        {
            try
            {
                var responseApiError = ValidateCU( model );

                if(responseApiError != null)
                {
                    return responseApiError;
                }

                responseApiError = Validate(model);

                if (responseApiError != null)
                {
                    return responseApiError;
                }


                var post = 
                    (from p in db.Posts where p.PostID == model.PostID select p)
                    .First();

                var usuario = 
                    (from u in db.Users where u.Id == model.UserID select u)
                    .First();

                var like = new Like 
                { 
                    PostID = post.PostID,
                    Post = post,
                    UserID = usuario.Id,
                    User = usuario
                };

                db.Add( like );

                db.SaveChanges();

                return null;
            }
            catch( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public ResponseApiError Delete( CULikeModel model )
        {
            try
            {
                var responseApiError = ValidateCU( model );

                if (responseApiError != null)
                {
                    return responseApiError;
                }


                var like = db.Likes.First( l => 
                    l.PostID == model.PostID && 
                    l.UserID == model.UserID
                );

                if (like == null)
                    return new ResponseApiError
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Like no encontrado"
                    };

                db.Remove( like );

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message
                };
            }
        }

        public List<Like> GetLikes()
        {
            try 
            {
                List<Like> likes = ( from l in db.Likes select l ).ToList();
                return likes;
            }
            catch( Exception ex )
            {
                throw ex;
            }
        }

        public int GetPostLikeCount( int id )
        {
            try
            {
                List<Like> likes = 
                    (from l in db.Likes where l.PostID == id select l)
                    .ToList();

                return likes.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
