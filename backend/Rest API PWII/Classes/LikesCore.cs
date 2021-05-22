using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        private IHostingEnvironment env;
        private HttpRequest request;

        public  LikesCore(PosThisDbContext db, IHostingEnvironment env, HttpRequest request)
        {
            this.db = db;
            this.env = env;
            this.request = request;
        }
        
        private ResponseApiError ValidateCreation(LikeViewModel model )
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

        private ResponseApiError ValidateDeletion( LikeViewModel model )
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

        public  ResponseApiError Create( LikeViewModel model, ref FeedPostModel feedPostModel )
        {
            try
            {
                var err = ValidateCreation(model);
                if ( err != null )
                    return err;

                var post = 
                    (from p in db.Posts where p.PostID == model.PostID select p)
                    .First();

                var user = 
                    (from u in db.Users where u.Id == model.UserID select u)
                    .First();

                var like = new Like 
                { 
                    PostID  = post.PostID,
                    Post    = post,
                    UserID  = user.Id,
                    User    = user
                };

                db.Add( like );

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
            catch( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = (int) HttpStatusCode.InternalServerError,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public  ResponseApiError Delete( LikeViewModel model, ref FeedPostModel feedPostModel )
        {
            try
            {
                var err = ValidateDeletion( model );

                if (err != null)
                    return err;

                var like = db.Likes.FirstOrDefault( l => 
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

        public  List<LikeViewModel> GetLikes()
        {
            var likes = ( from l in db.Likes 
                          select new LikeViewModel
                          {
                            PostID = l.PostID,
                            UserID = l.UserID
                          } ).ToList();
            return likes;
        }

        public  List<LikeViewModel> GetPostLikes( int id )
        {
            var likes = 
                (from l in db.Likes
                 where l.PostID == id 
                 select new LikeViewModel {
                     PostID = l.PostID,
                     UserID = l.UserID
                 })
                .ToList();

            return likes;
        }
    }
}
