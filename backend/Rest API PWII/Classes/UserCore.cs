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

        public ResponseApiError ValidateUpdate(UpdateUserViewModel model )
        {
            if (string.IsNullOrEmpty( model.UserName )  &&
                string.IsNullOrEmpty( model.Tag      )  &&
                string.IsNullOrEmpty( model.Email    )  &&
                model.ProfilePic?.Length == 0           &&
                model.CoverPic  ?.Length == 0)
                    return new ResponseApiError
                    {
                        Code            = (int)HttpStatusCode.BadRequest,
                        HttpStatusCode  = (int)HttpStatusCode.BadRequest,
                        Message         = "Must edit at least one field" 
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
            query = query.ToLower().Trim();

            var searchResultModel = new SearchResultModel();

            if ( model.SearchPosts )
                searchResultModel.posts =
                    (from p 
                     in db.Posts
                        .Include( x=>x.Medias  )
                        .Include( x=>x.Replies )
                        .Include( X=>X.Likes   )
                        .Include( x=>x.Reposts )
                     where p.Content.ToLower().Contains( query )
                     orderby p.PostDate descending
                     select new SearchResultPostModel
                     {
                         PostID              = p.PostID,
                         Content             = p.Content,
                         PublisherID         = p.User.Id,
                         PublisherUserName   = p.User.UserName,
                         PublisherTag        = p.User.Tag,
                         PublisherProfilePic = p.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}" : null,
                         Date                = p.PostDate,
                         LikeCount           = p.Likes.Count,
                         ReplyCount          = p.Replies.Count,
                         RepostCount         = p.Reposts.Count,
                         IsLiked             = p.Likes.FirstOrDefault(l => l.UserID == model.UserID) != null,
                         IsReposted          = p.Reposts.FirstOrDefault(l => l.UserID == model.UserID) != null,
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
                (from p in db.Posts
                                .Include(x => x.User   )
                                .Include(x => x.Medias )
                                .Include(X => X.Likes  )
                                .Include(x => x.Replies)
                                .Include(x => x.Reposts)
                 orderby p.PostDate descending,
                         (following.Contains( p.User.Id )) descending
                 select new FeedPostModel {
                     PostID                 = p.PostID,
                     Content                = p.Content,
                     PublisherID            = p.User.Id,
                     PublisherUserName      = p.User.UserName,
                     PublisherTag           = p.User.Tag,
                     PublisherProfilePic    = p.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}" : null,
                     Date                   = p.PostDate,
                     LikeCount              = p.Likes.Count,
                     ReplyCount             = p.Replies.Count,
                     RepostCount            = p.Reposts.Count,
                     ReposterID             = null,
                     ReposterUserName       = null,
                     IsRepost               = false,
                     IsLiked                = p.Likes   .FirstOrDefault( l => l.UserID == id ) != null,
                     IsReposted             = p.Reposts .FirstOrDefault( l => l.UserID == id ) != null,
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
                    .Include(x => x.Post)
                    .Include(x => x.Post.Likes)
                    .Include(x => x.Post.Replies)
                    .Include(x => x.Post.Reposts)
                    .Include(x => x.Post.Medias)
                    .Include(x => x.Post.User)
                    .Include(x => x.User)
                 orderby rp.RepostDate descending,
                         (following.Contains(rp.User.Id)) descending
                 where id != rp.UserID
                 select new FeedPostModel
                 {
                     PostID              = rp.Post.PostID,
                     Content             = rp.Post.Content,
                     PublisherID         = rp.Post.User.Id,
                     PublisherUserName   = rp.Post.User.UserName,
                     PublisherTag        = rp.Post.User.Tag,
                     PublisherProfilePic = rp.Post.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{rp.Post.User.ProfilePic.Name}" : null,
                     Date                = rp.RepostDate,
                     LikeCount           = rp.Post.Likes.Count,
                     ReplyCount          = rp.Post.Replies.Count,
                     RepostCount         = rp.Post.Reposts.Count,
                     ReposterID          = rp.User.Id,
                     ReposterUserName    = rp.User.UserName,
                     IsRepost            = true,
                     IsLiked             = rp.Post.Likes.FirstOrDefault(l => l.UserID == id)   != null,
                     IsReposted          = rp.Post.Reposts.FirstOrDefault(l => l.UserID == id) != null,
                     Medias  = (from m in rp.Post.Medias
                                select new MediaViewModel
                                {
                                    MediaID = m.MediaID,
                                    MIME = m.MIME,
                                    Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                    IsVideo = m.MIME.Contains("video")
                                }).ToList()
                 }).ToList();

            var query = posts.AsQueryable()
                            .Union(reposts.AsQueryable());

            Func<IGrouping<int, FeedPostModel>, FeedPostModel> GetReposted =
                groupedFP => (from g in groupedFP
                              where g.IsRepost == true
                              select g
                              ).FirstOrDefault();

            Func<IGrouping<int, FeedPostModel>, FeedPostModel> GetNotReposted =
                groupedFP => (from g in groupedFP
                              where g.IsRepost == false
                              select g
                              ).FirstOrDefault();

            Func<IGrouping<int, FeedPostModel>, bool> IsReposted = groupedFP => GetReposted(groupedFP) != null;

            var feed = (from fp in query
                        group fp by fp.PostID into groupedFP
                        select new FeedPostModel {
                            PostID              = groupedFP.Key,
                            IsRepost            = groupedFP.Any( fp => fp.IsRepost ),
                            Content             = groupedFP.First().Content,
                            PublisherID         = groupedFP.First().PublisherID,
                            PublisherUserName   = groupedFP.First().PublisherUserName,
                            PublisherTag        = groupedFP.First().PublisherTag,
                            PublisherProfilePic = groupedFP.First().PublisherProfilePic,
                            Date                = IsReposted(groupedFP) ? GetReposted(groupedFP).Date : GetNotReposted(groupedFP).Date,
                            LikeCount           = groupedFP.First().LikeCount,
                            ReplyCount          = groupedFP.First().ReplyCount,
                            RepostCount         = groupedFP.First().RepostCount,
                            ReposterID          = IsReposted(groupedFP) ? GetReposted(groupedFP).ReposterID         : null,
                            ReposterUserName    = IsReposted(groupedFP) ? GetReposted(groupedFP).ReposterUserName   : null,
                            IsLiked             = groupedFP.First().IsLiked,
                            IsReposted          = groupedFP.First().IsReposted,
                            Medias = groupedFP.First().Medias
                        }).OrderByDescending(x=>x.Date).ToList();

            return feed;
        }

        public List<FeedPostModel> GetUserPosts(string id, string viewerId)
        {
            var err = ValidateExists(id);
            if (err != null)
                return null;

            var posts =
                (from p in db.Posts
                                .Include(x => x.User)
                                .Include(x => x.Medias)
                                .Include(X => X.Likes)
                                .Include(x => x.Replies)
                                .Include(x => x.Reposts)
                 where id == p.User.Id
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
                     IsLiked = p.Likes.FirstOrDefault(l => l.UserID == viewerId) != null,
                     IsReposted = p.Reposts.FirstOrDefault(l => l.UserID == viewerId) != null,
                     Medias = (from m in p.Medias
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
                   .Include(x => x.Post)
                   .Include(x => x.Post.Likes)
                   .Include(x => x.Post.Replies)
                   .Include(x => x.Post.Reposts)
                   .Include(x => x.Post.Medias)
                   .Include(x => x.Post.User)
                   .Include(x => x.User)
                where rp.User.Id == id      &&
                    rp.User.Id != viewerId  && 
                    rp.Post.User.Id != id
                select new FeedPostModel
                {
                    PostID = rp.Post.PostID,
                    Content = rp.Post.Content,
                    PublisherID = rp.Post.User.Id,
                    PublisherUserName = rp.Post.User.UserName,
                    PublisherTag = rp.Post.User.Tag,
                    PublisherProfilePic = rp.Post.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{rp.Post.User.ProfilePic.Name}" : null,
                    Date = rp.RepostDate,
                    LikeCount = rp.Post.Likes.Count,
                    ReplyCount = rp.Post.Replies.Count,
                    RepostCount = rp.Post.Reposts.Count,
                    ReposterID = rp.User.Id,
                    ReposterUserName = rp.User.UserName,
                    IsRepost = true,
                    IsLiked = rp.Post.Likes.FirstOrDefault(l => l.UserID == viewerId) != null,
                    IsReposted = rp.Post.Reposts.FirstOrDefault(l => l.UserID == viewerId) != null,
                    Medias = (from m in rp.Post.Medias
                              select new MediaViewModel
                              {
                                  MediaID = m.MediaID,
                                  MIME = m.MIME,
                                  Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                  IsVideo = m.MIME.Contains("video")
                              }).ToList()
                }).ToList();

            var query = posts.AsQueryable()
                            .Union(reposts.AsQueryable());

            Func<IGrouping<int, FeedPostModel>, FeedPostModel> GetReposted =
                groupedFP => (from g in groupedFP
                              where g.IsRepost == true
                              select g
                              ).FirstOrDefault();

            Func<IGrouping<int, FeedPostModel>, FeedPostModel> GetNotReposted =
                groupedFP => (from g in groupedFP
                              where g.IsRepost == false
                              select g
                              ).FirstOrDefault();

            Func<IGrouping<int, FeedPostModel>, bool> IsReposted = groupedFP => GetReposted(groupedFP) != null;

            var feed = (from  fp in query
                        group fp by fp.PostID into groupedFP
                        select new FeedPostModel
                        {
                            PostID              = groupedFP.Key,
                            IsRepost            = groupedFP.Any(fp => fp.IsRepost),
                            Content             = groupedFP.First().Content,
                            PublisherID         = groupedFP.First().PublisherID,
                            PublisherUserName   = groupedFP.First().PublisherUserName,
                            PublisherTag        = groupedFP.First().PublisherTag,
                            PublisherProfilePic = groupedFP.First().PublisherProfilePic,
                            Date                = IsReposted(groupedFP) ? GetReposted(groupedFP).Date : GetNotReposted(groupedFP).Date,
                            LikeCount           = groupedFP.First().LikeCount,
                            ReplyCount          = groupedFP.First().ReplyCount,
                            RepostCount         = groupedFP.First().RepostCount,
                            ReposterID          = IsReposted(groupedFP) ? GetReposted(groupedFP).ReposterID : null,
                            ReposterUserName    = IsReposted(groupedFP) ? GetReposted(groupedFP).ReposterUserName : null,
                            IsLiked             = groupedFP.First().IsLiked,
                            IsReposted          = groupedFP.First().IsReposted,
                            Medias              = groupedFP.First().Medias
                        }).OrderByDescending(fp=>fp.Date).ToList();

            return feed;
        }

        public UserViewModel    GetOne( string id, string viewerId)
        {
            return (from u
                    in db.Users
                            .Include(x=>x.Follows)
                            .Include(x=>x.Following)
                    where u.Id == id
                    select new UserViewModel
                    {
                        Id              = u.Id,
                        UserName        = u.UserName,
                        Tag             = u.Tag,
                        Email           = u.Email,
                        BirthDate       = u.BirthDate,
                        ProfilePicPath  = u.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.ProfilePic.Name}" : null,
                        CoverPicPath    = u.CoverPic   != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.CoverPic.Name}"   : null,
                        FollowerCount   = u.Follows.Count,
                        FollowingCount  = u.Following.Count,
                        IsFollowed      = u.Follows.FirstOrDefault( x=> x.UserFollowerID == (viewerId != null ? viewerId : "" ) ) != null
                    }).FirstOrDefault();
        }

        public ResponseApiError Update( string id, UpdateUserViewModel model, ref UserViewModel outModel )
        {
            try
            {
                var err = ValidateUpdate( model );
                if (err != null)
                    return err;

                err = ValidateExists( id );
                if (err != null)
                    return err;

                var user = db.Users.First( u => u.Id == id );

                user.Tag  = model.Tag != null ? model.Tag : user.Tag;

                user.UserName             = model.UserName != null ? model.UserName : user.UserName;
                user.NormalizedUserName   = model.UserName != null ? model.UserName.ToUpper() : user.NormalizedUserName;

                user.Email            = model.Email != null ? model.Email : user.Email;
                user.NormalizedEmail  = model.Email != null ? model.Email.ToUpper() : user.NormalizedEmail;

                db.SaveChanges();
                db.Attach( user );

                if ( model.ProfilePic == null && model.CoverPic == null )
                    return null;

                var mediaCore = new MediaCore(db, env, request);
                
                if( model.ProfilePic != null)
                {
                    if( user.ProfilePic != null)
                        mediaCore.DeleteUserMedia( user.ProfilePic.MediaID );

                    var um = new UserMedia();
                    mediaCore.CreateUserMedia( model.ProfilePic, ref um );
                    user.ProfilePic = um;
                    user.ProfilePicID = um.MediaID;
                }

                if ( model.CoverPic != null )
                {
                    if ( user.CoverPic != null )
                        mediaCore.DeleteUserMedia(user.CoverPic.MediaID);

                    var um = new UserMedia();
                    mediaCore.CreateUserMedia( model.CoverPic, ref um );
                    user.CoverPic = um;
                    user.CoverPicID = um.MediaID;
                }

                db.SaveChanges();

                outModel = (from u in db.Users
                                            .Include(u => u.Follows   )
                                            .Include(u => u.Following )
                                            .Include(u => u.ProfilePic)
                                            .Include(u => u.CoverPic  )
                            where id == u.Id
                            select new UserViewModel
                            {
                                Id              = u.Id,
                                UserName        = u.UserName,
                                Tag             = u.Tag,
                                Email           = u.Email,
                                ProfilePicPath  = u.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.ProfilePic.Name}" : null,
                                CoverPicPath    = u.CoverPic != null   ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{u.CoverPic.Name}" : null,
                                BirthDate       = u.BirthDate,
                                FollowerCount   = u.Follows   != null ? u.Follows.Count   : 0,
                                FollowingCount  = u.Following != null ? u.Following.Count : 0
                            }).First();

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

                err = mediaCore.DeleteUserMedia(user.CoverPicID.Value);
                if (err != null)
                    return err;
                
                user.CoverPic = um;

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
