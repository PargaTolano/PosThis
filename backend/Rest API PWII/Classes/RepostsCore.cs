﻿using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Rest_API_PWII.Classes
{
    public class RepostsCore
    {
        private PosThisDbContext db;

        public RepostsCore( PosThisDbContext db )
        {
            this.db = db;
        }

        public ResponseApiError Validate( CRepostModel model )
        {
            if ( model.PostID == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "PostID does not accept null"
                };

            var post = db.Posts.FirstOrDefault(p => p.PostID == model.PostID);
            if(post == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Post does not exist"
                };

            if ( model.UserID == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "UserID does not accept null"
                };
            
            var user = db.Users.FirstOrDefault(u => u.Id == model.UserID);
            if (user == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User does not exist"
                };

            var repost = db.Reposts.FirstOrDefault(rp => 
                            rp.UserID == model.UserID && 
                            rp.PostID == model.PostID);
            if ( repost != null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Repost already exists"
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var repost = db.Reposts.FirstOrDefault(rp => rp.RepostID == id);

            if( repost == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Repost not found in database"
                };

            return null;
        }

        public List<Reposts> Get()
        {
            try
            {
                List<Reposts> reposts = 
                    (from rp in db.Reposts select rp)
                    .DefaultIfEmpty()
                    .ToList();

                return reposts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        public ResponseApiError Create( CRepostModel model )
        {
            try
            {
                var err = Validate( model );

                if ( err != null )
                    return err;

                var post    = db.Posts.First( p => p.PostID == model.PostID );
                var user = db.Users.First(u => u.Id == model.UserID);

                var repost = new Reposts 
                    {
                        PostID = (int)model.PostID,
                        Post = post,
                        UserID = model.UserID,
                        User = user
                    };

                db.Reposts.Add( repost );

                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public ResponseApiError Delete( int id )
        {
            try
            {
                var err = ValidateExists( id );

                if (err != null)
                    return err;

                var repost = db.Reposts.FirstOrDefault( rp => rp.RepostID == id );

                db.Remove( repost );

                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = ( int ) HttpStatusCode.InternalServerError,
                    HttpStatusCode = ( int ) HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}
