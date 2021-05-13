using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

using Microsoft.EntityFrameworkCore;

namespace Rest_API_PWII.Classes
{
    public class UserCore
    {
        private PosThisDbContext    db;
        private IHostingEnvironment env;
        private HttpRequest         request;

        public UserCore(PosThisDbContext db, IHostingEnvironment env, HttpRequest request)
        {
            this.db = db;
            this.env = env;
            this.request = request;
        }

        public ResponseApiError Validate( User user )
        {
            if ( user.UserName == null || user.Tag == null || user.Email == null )
                return new ResponseApiError { 
                    Code = 1,
                    HttpStatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "User data not valid" 
                };

            return null;
        }

        public ResponseApiError ValidateUpdate( UserViewModel model )
        {
            if( 
                model.UserName  == null &&
                model.Tag       == null &&
                model.Email     == null &&
                model.BirthDate == null 
                )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Must edit at least one field" 
                };

            return null;
        }

        public ResponseApiError ValidateNewPassword( User user )
        {
            if ( user.PasswordHash == null )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User data not valid"
                };

            return null;
        }

        public ResponseApiError ValidateExists( User user )
        {
            var res = (from u in db.Users where u.Id == user.Id select u).First();

            if (res == null)
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound, 
                    Message = "User not found in database" 
                };

            return null;
        }

        public ResponseApiError ValidateExists( string id )
        {
            var res = (from u in db.Users where u.Id == id select u).FirstOrDefault();

            if ( res == null )
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found in database"
                };

            return null;
        }

        public bool ValidateSearch ( SearchRequestModel model )
        {
            return model.SearchPosts || model.SearchUsers;
        }
        
        public ResponseApiError Create( User user )
        {
            try
            {
                ResponseApiError err = Validate(user);
                if (err != null)
                    return err;

                db.Users.Add( user );
                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                return new ResponseApiError { 
                    Code = 3, 
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public List<UserViewModel> GetAll()
        {
            var users = 
                (from u 
                 in db.Users.Include(x=>x.ProfilePic)
                 select new UserViewModel
                 {
                     Id = u.Id,
                     UserName = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     BirthDate = u.BirthDate,
                     ProfilePicPath = u.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.ProfilePic.Name}" : null,
                 }).ToList();

            return users;
        }

        public SearchResultModel GetSearch( SearchRequestModel model )
        {
            var valid = ValidateSearch( model );

            if ( !valid )
                return null;

            var query = model.Query != null ? model.Query : "";

            var searchResultModel = new SearchResultModel();

            if ( model.SearchPosts )
                searchResultModel.posts =
                    (from p 
                     in db.Posts
                        .Include( x=>x.Medias  )
                        .Include( X=>X.Likes   )
                        .Include( x=>x.Replies )
                        .Include( x=>x.Reposts )
                     where p.Content.ToLower().Contains( query.ToLower().Trim() )
                     select new SearchResultPostModel
                     {
                         PostID              = p.PostID,
                         Content             = p.Content,
                         PublisherID         = p.User.Id,
                         PublisherUserName   = p.User.UserName,
                         PublisherTag        = p.User.Tag,
                         PublisherProfilePic = p.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}" : null,
                         PublishingTime      = p.PostDate,
                         LikeCount           = p.Likes.Count,
                         ReplyCount          = p.Replies.Count,
                         RepostCount         = p.Reposts.Count,
                         Medias              = ( from m in p.Medias
                                                 select new MediaViewModel
                                                 {
                                                     MediaID = m.MediaID,
                                                     MIME = m.MIME,
                                                     Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                                     IsVideo = m.MIME.Contains("video")
                                                 }).ToList()
                     }).ToList();

            if ( model.SearchUsers )
                searchResultModel.users =
                    (from  u in db.Users.Include(x=>x.Follows).Include( x => x.ProfilePic)
                     where u.NormalizedUserName.Contains( query.ToUpper() )
                     select new SearchResultUserModel
                     {
                         UserId         = u.Id,
                         UserName       = u.UserName,
                         UserTag        = u.Tag,
                         ProfilePicPath = u.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.ProfilePic.Name}" : null,
                         FollowerCount  = u.Follows.Count
                     }).ToList();

            return searchResultModel;
        }

        public List<FeedPostModel> GetFeed( string id )
        {
            var err = ValidateExists( id );
            if ( err != null )
                return null;

            var following = (from f in db.Follows
                             where f.UserFollowerID == id
                             select f.UserFollowID).ToList();

            var posts =
                (from p in db.Posts.Include(x=>x.Medias)
                 orderby p.PostDate descending,
                         (following.Contains( p.User.Id )) descending
                 select new FeedPostModel {
                     IsRepost       = false,
                     Content        = p.Content,
                     PublisherID    = p.User.Id,
                     ReposterID     = null,
                     PostID         = p.PostID,
                     Date           = p.PostDate,
                     Medias       = (from m in p.Medias
                                     select new MediaViewModel
                                     {
                                         MediaID = m.MediaID,
                                         MIME = m.MIME,
                                         Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                         IsVideo = m.MIME.Contains("video")
                                     }).ToList()

                 }).ToList();
            
             var reposts = 
                (from rp 
                 in db.Reposts
                    .Include(x=>x.Post)
                    .Include(x=>x.Post.Medias)
                    .Include(x=>x.User)
                 orderby rp.RepostDate descending,
                         (following.Contains(rp.User.Id)) descending
                 where id != rp.UserID
                 select new FeedPostModel
                 {
                     IsRepost        = true,
                     Content         = rp.Post.Content,
                     PublisherID     = rp.Post.User.Id,
                     ReposterID      = rp.User.Id,
                     PostID          = rp.PostID,
                     Date            = rp.RepostDate,
                     Medias        = (from m in rp.Post.Medias
                                      select new MediaViewModel
                                      {
                                          MediaID = m.MediaID,
                                          MIME = m.MIME,
                                          Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                          IsVideo = m.MIME.Contains("video")
                                      }).ToList()
                 }).ToList();

            var pQuery = posts.AsQueryable();
            var rpQuery = reposts.AsQueryable();

            var rQuery = pQuery.Union(rpQuery);
            rQuery = rQuery.OrderByDescending(x => x.Date);

            var feed = (from fp in rQuery
                        group fp by fp.PostID into groupedFP
                        select new FeedPostModel { 
                            IsRepost    = groupedFP.Any( fp => fp.IsRepost ),
                            Content     = groupedFP.First().Content,
                            PublisherID = groupedFP.First().PublisherID,
                            ReposterID  = groupedFP.First().ReposterID,
                            PostID      = groupedFP.First().PostID,
                            Date        = groupedFP.First().Date,
                            Medias    = groupedFP.First().Medias
                        });

            return feed.ToList();
        }

        public List<FeedPostModel> GetUserPosts(string id)
        {
            var err = ValidateExists(id);
            if (err != null)
                return null;

            var following = (from f in db.Follows
                             where f.UserFollowerID == id
                             select f.UserFollowID).ToList();

            var posts =
                (from p in db.Posts
                                .Include(x => x.Medias)
                 orderby p.PostDate descending,
                         (following.Contains(p.User.Id)) descending
                 where id == p.User.Id
                 select new FeedPostModel
                 {
                     IsRepost       = false,
                     Content        = p.Content,
                     PublisherID    = p.User.Id,
                     ReposterID     = null,
                     PostID         = p.PostID,
                     Date           = p.PostDate,
                     Medias = (from m in p.Medias
                                select new MediaViewModel { 
                                   MediaID = m.MediaID,
                                   MIME    = m.MIME,
                                   Path    = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                   IsVideo = m.MIME.Contains("video")
                                }).ToList()

                 }).ToList();

            var reposts =
               (from rp
                in db.Reposts
                   .Include(x => x.Post)
                   .Include(x => x.Post.Medias)
                   .Include(x => x.User)
                orderby rp.RepostDate descending,
                        (following.Contains(rp.User.Id)) descending
                where id == rp.UserID
                select new FeedPostModel
                {
                    IsRepost = true,
                    Content = rp.Post.Content,
                    PublisherID = rp.Post.User.Id,
                    ReposterID = rp.User.Id,
                    PostID = rp.PostID,
                    Date = rp.RepostDate,
                    Medias = (from m in rp.Post.Medias
                                select new MediaViewModel
                                {
                                    MediaID = m.MediaID,
                                    MIME = m.MIME,
                                    Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                    IsVideo = m.MIME.Contains("video")
                                }).ToList()
                }).ToList();

            var pQuery = posts.AsQueryable();
            var rpQuery = reposts.AsQueryable();

            var rQuery = pQuery.Union(rpQuery);
            rQuery = rQuery.OrderByDescending(x => x.Date);

            var feed = (from fp in rQuery
                        group fp by fp.PostID into groupedFP
                        select new FeedPostModel
                        {
                            IsRepost = groupedFP.Any(fp => fp.IsRepost),
                            Content = groupedFP.First().Content,
                            PublisherID = groupedFP.First().PublisherID,
                            ReposterID = groupedFP.First().ReposterID,
                            PostID = groupedFP.First().PostID,
                            Date = groupedFP.First().Date,
                            Medias = groupedFP.First().Medias
                        });

            return feed.ToList();
        }

        public UserViewModel GetOne( string id )
        {
            return (from u
                    in db.Users
                    where u.Id == id
                    select new UserViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Tag = u.Tag,
                        Email = u.Email,
                        BirthDate = u.BirthDate,
                        ProfilePicPath = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.ProfilePic.Name}"
                    }).FirstOrDefault();
        }

        public ResponseApiError Update( string id, UserViewModel user )
        {
            try
            {
                var err = ValidateUpdate( user );
                if (err != null)
                    return err;

                err = ValidateExists( id );
                if (err != null)
                    return err;

                User userDb = db.Users.First( u => u.Id == id );

                userDb.Tag       = user.Tag != null ? user.Tag : userDb.Tag;

                userDb.UserName  = user.UserName != null ? user.UserName : userDb.UserName;
                userDb.NormalizedUserName = user.UserName != null ? user.UserName.ToUpper() : userDb.NormalizedUserName;

                userDb.Email     = user.Email != null ? user.Email : userDb.Email;
                userDb.NormalizedEmail = user.Email != null ? user.Email.ToUpper() : userDb.NormalizedEmail;

                userDb.BirthDate = user.BirthDate != null ? user.BirthDate : userDb.BirthDate;

                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public ResponseApiError UploadProfilePic( string id, IFormFile file )
        {
            try
            {
                var mediaCore = new MediaCore( db, env, request );
                var um = new UserMedia();

                var err = mediaCore.CreateUserMedia( file, ref um );
                if ( err != null )
                    return err;

                err = ValidateExists( id );
                if ( err != null )
                    return err;

                var user = db.Users.First( u => u.Id == id );

                db.Attach( user );
                user.ProfilePic = um;
                user.ProfilePicID = um.MediaID;

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public ResponseApiError UploadCoverPic( string id, IFormFile file )
        {
            try
            {
                var mediaCore = new MediaCore( db, env, request );
                var um = new UserMedia();

                var err = mediaCore.CreateUserMedia( file, ref um );
                if ( err != null )
                    return err;

                err = ValidateExists( id );
                if ( err != null )
                    return err;

                var user = db.Users.First(u => u.Id == id);

                db.Attach( user );
                user.CoverPic = um;
                user.CoverPicID = um.MediaID;

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public ResponseApiError Delete( string id )
        {
            try
            {
                var err = ValidateExists( id );
                if (err != null)
                    return err;

                User userDb = db.Users.First( u => u.Id == id );

                db.Users.Remove( userDb );

                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }  
    }
}
