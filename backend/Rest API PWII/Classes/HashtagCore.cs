using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class HashtagCore
    {
        private PosThisDbContext db;

        public HashtagCore( PosThisDbContext db )
        {
            this.db = db;
        }

        public ResponseApiError Validate( HashtagViewModel model )
        {
            if ( string.IsNullOrWhiteSpace( model.Content ) )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Hashtag can not be empty"
                };

            return null;
        }

        public bool ValidateExists( HashtagViewModel model )
        {
            var res = 
                (from h
                in db.Hashtags
                where h.ContentHashtag == model.Content
                select h).FirstOrDefault();

            return res != null;
        }

        public ResponseApiError ValidatePostExists( HashtagViewModel model )
        {
            var res =
                (from p
                in db.Posts
                where p.PostID == model.PostID
                select p).FirstOrDefault();

            if ( res == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Hashtag can not be empty"
                };

            return null;
        }

        public List<Hashtag> GetAll()
        {
            List<Hashtag> hashtags = 
                (from u in db.Hashtags select u).ToList();
            return hashtags;
        }

        public List<Post> GetPostsWithHashtag( string texto )
        {
            try
            {
                var hastagDb =
                    (from h
                     in db.Hashtags
                     where texto == h.ContentHashtag
                     select h).FirstOrDefault();

                if ( hastagDb == null )
                    throw new Exception("Hashtag does not exist");

                var posts =
                    (from hp
                     in db.HashtagPosts
                     where texto == hp.Hashtag.ContentHashtag
                     select hp.Post).DefaultIfEmpty().ToList();

                if ( posts == null )
                    posts = new List<Post>();

                return posts;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public ResponseApiError Create( HashtagViewModel model )
        {
            try
            {
                var err = Validate( model );
                if ( err != null )
                    return err;

                err = ValidatePostExists( model );
                if ( err != null )
                    return err;

                Hashtag hashtagDb;

                var exists = ValidateExists( model );
                if ( !exists ) {

                    var hashtag = new Hashtag { ContentHashtag = model.Content };

                    var entry = db.Hashtags.Add( hashtag );
                    hashtagDb = entry.Entity;

                    if ( hashtagDb == null )
                        throw new Exception("Create hashtag failed");
                }
                else
                    hashtagDb = db.Hashtags.First( h => h.ContentHashtag == model.Content );

                var post = db.Posts.First( p => p.PostID == model.PostID );

                var hp = new HashtagPost 
                    {
                        HashtagID = hashtagDb.HashtagID,
                        Hashtag = hashtagDb,
                        PostID = post.PostID,
                        Post = post
                    };

                db.HashtagPosts.Add( hp );
                
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
    }
}
