using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Rest_API_PWII.Classes
{
    public class RepostsCore
    {
        private PosThisDbContext    db;
        private IHostingEnvironment env;
        private HttpRequest         request;

        public RepostsCore( PosThisDbContext db, IHostingEnvironment env, HttpRequest request )
        {
            this.db = db;
            this.env = env;
            this.request = request;
        }

        public ResponseApiError Validate( RepostViewModel model )
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
                    Code            = (int)HttpStatusCode.NotFound,
                    HttpStatusCode  = (int)HttpStatusCode.NotFound,
                    Message         = "Repost not found in database"
                };

            return null;
        }

        public List<RepostViewModel> Get()
        {
            var reposts = 
                (from rp in db.Reposts
                 select new RepostViewModel 
                {
                     UserID = rp.UserID,
                     PostID = rp.PostID,
                }).ToList();

            return reposts;
        } 
        
        public ResponseApiError Create( RepostViewModel model, ref FeedPostModel feedPostModel)
        {
            try
            {
                var err = Validate( model );

                if ( err != null )
                    return err;

                var post    = db.Posts.First( p => p.PostID == model.PostID );
                var user = db.Users.First(u => u.Id == model.UserID);

                var repost = new Repost 
                    {
                        PostID = (int)model.PostID,
                        Post = post,
                        UserID = model.UserID,
                        User = user,
                        RepostDate = DateTime.Now
                    };

                db.Reposts.Add( repost );

                db.SaveChanges();

                feedPostModel = (from p in db.Posts
                                .Include(x => x.User)
                                .Include(x => x.Medias)
                                .Include(X => X.Likes)
                                .Include(x => x.Replies)
                                .Include(x => x.Reposts)
                                 where p.PostID == model.PostID
                                 select new FeedPostModel
                                 {
                                     PostID = p.PostID,
                                     Content = p.Content,
                                     PublisherID = p.User.Id,
                                     PublisherUserName = p.User.UserName,
                                     PublisherTag = p.User.Tag,
                                     PublisherProfilePic = p.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}" : null,
                                     Date = p.PostDate,
                                     LikeCount = p.Likes.Count,
                                     ReplyCount = p.Replies.Count,
                                     RepostCount = p.Reposts.Count,
                                     ReposterID = null,
                                     ReposterUserName = null,
                                     IsRepost = false,
                                     IsLiked = p.Likes.FirstOrDefault(l => l.UserID == model.UserID) != null,
                                     IsReposted = p.Reposts.FirstOrDefault(l => l.UserID == model.UserID) != null,
                                     Medias = (from m in p.Medias
                                               select new MediaViewModel
                                               {
                                                   MediaID = m.MediaID,
                                                   MIME = m.MIME,
                                                   Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                                   IsVideo = m.MIME.Contains("video")
                                               }).ToList()

                                 }).FirstOrDefault();

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

        public ResponseApiError Delete( RepostViewModel model, ref FeedPostModel feedPostModel )
        {
            try
            {
                var err = Validate(model) ;

                if (err != null)
                    return err;

                var repost = db.Reposts.FirstOrDefault( rp => rp.PostID == model.PostID && rp.UserID == model.UserID);

                db.Remove( repost );

                db.SaveChanges();

                feedPostModel = (from p in db.Posts
                                .Include(x => x.User)
                                .Include(x => x.Medias)
                                .Include(X => X.Likes)
                                .Include(x => x.Replies)
                                .Include(x => x.Reposts)
                                 where p.PostID == model.PostID
                                 select new FeedPostModel
                                 {
                                     PostID = p.PostID,
                                     Content = p.Content,
                                     PublisherID = p.User.Id,
                                     PublisherUserName = p.User.UserName,
                                     PublisherTag = p.User.Tag,
                                     PublisherProfilePic = p.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}" : null,
                                     Date = p.PostDate,
                                     LikeCount = p.Likes.Count,
                                     ReplyCount = p.Replies.Count,
                                     RepostCount = p.Reposts.Count,
                                     ReposterID = null,
                                     ReposterUserName = null,
                                     IsRepost = false,
                                     IsLiked = p.Likes.FirstOrDefault(l => l.UserID == model.UserID) != null,
                                     IsReposted = p.Reposts.FirstOrDefault(l => l.UserID == model.UserID) != null,
                                     Medias = (from m in p.Medias
                                               select new MediaViewModel
                                               {
                                                   MediaID = m.MediaID,
                                                   MIME = m.MIME,
                                                   Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                                   IsVideo = m.MIME.Contains("video")
                                               }).ToList()

                                 }).FirstOrDefault();

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