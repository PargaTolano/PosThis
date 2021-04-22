﻿using Rest_API_PWII.Models;
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
                    Message = "Liked post can not be null",
                };

            if ( model.UserID == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Like's user can not be null",
                };

            var post = db.Posts.FirstOrDefault(p => p.PostID == model.PostID);

            if ( post == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Post does not exist, can not like",
                };

            var user = db.Users.FirstOrDefault(u => u.Id == model.UserID );

            if (user == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User does not exist, can not like",
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
                    Message = "Like already exists",
                };

            return null;
        }

        private ResponseApiError ValidateCU( CULikeModel model )
        {
            if ( model.PostID == null )
                return new ResponseApiError 
                { 
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Like's PostID can not be null", 
                    HttpStatusCode = (int)HttpStatusCode.BadRequest 
                };

            if ( model.UserID == null )  
                return new ResponseApiError 
                { 
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Like's UserID can not be bull", 
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

                var user = 
                    (from u in db.Users where u.Id == model.UserID select u)
                    .First();

                var like = new Like 
                { 
                    PostID = post.PostID,
                    Post = post,
                    UserID = user.Id,
                    User = user
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
                        Message = "Like not found"
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
