using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class PostCore
    {
        private PosThisDbContext    db;
        private IHostingEnvironment env;
        private HttpRequest         request;

        public PostCore(PosThisDbContext db, IHostingEnvironment env, HttpRequest request)
        {
            this.db = db;
            this.env = env;
            this.request = request;
        }

        public ResponseApiError Validate( Post post )
        {
            if ( post.Content == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post data not valid, must have text content and/or media"
                };

            return null;
        }

        public ResponseApiError ValidateC( CPostModel model )
        {
            bool textoValido = !string.IsNullOrEmpty(model.Content);
            bool mediaValido = model.Files?.Count > 0;

            if (textoValido || mediaValido)
                return null;

            return new ResponseApiError
            {
                Code = (int)HttpStatusCode.BadRequest,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Invalid post, must have text centent and at least one media file"
            };
        }

        public ResponseApiError ValidateU( int id, UPostModel model )
        {
            bool textoValido = !string.IsNullOrEmpty(model.Content);

            int count = (from p in db.Posts.Include(x=>x.Medias) where id == p.PostID select p).Count();
            bool mediaValido = model.Deleted.Count < count;

            if (textoValido || mediaValido)
                return null;

            return new ResponseApiError
            {
                Code            = (int)HttpStatusCode.BadRequest,
                HttpStatusCode  = (int)HttpStatusCode.BadRequest,
                Message         = "Invalid post, must have text content or at least one media file"
            };
        }

        public ResponseApiError ValidateExists( Post post )
        {
            var res = ( from p in db.Posts where p.PostID == post.PostID select p ).FirstOrDefault();

            if ( res == null )
                return new ResponseApiError {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int) HttpStatusCode.NotFound,
                    Message = "Post does not exist in database" 
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var res = (from p in db.Posts where p.PostID == id select p).FirstOrDefault();

            if ( res == null )
                return new ResponseApiError {
                    Code = (int)HttpStatusCode.NotFound, 
                    HttpStatusCode = (int) HttpStatusCode.NotFound,
                    Message = "Post does not exist in database"
                };


            return null;
        }

        public ResponseApiError Create( CPostModel model )
        {
            try
            {
                var err = ValidateC( model );
                if (err != null)
                    return err;

                var userDb = db.Users.FirstOrDefault( u => u.Id == model.UserID );
                if (userDb == null)
                    return new ResponseApiError
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = "User not found"
                    };

                var list = new List<PostMedia>();

                var mediaCore = new MediaCore( db, env, request );
                if( model.Files != null)
                {
                    err = mediaCore.CreatePostMedia( model.Files, ref list );
                    if (err != null)
                        return err;
                }
                    
                
                if (list == null && model.Files?.Count != 0)
                    throw new Exception("Error subiendo media");

                var post = new Post
                {
                    User = userDb,
                    Content = model.Content,
                    PostDate = DateTime.Now
                };

                db.Posts.Add(post);
                db.SaveChanges();

                db.Attach(post);

                post.Medias = list;

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

        public List<PostViewModel> GetAll()
        {
            var posts = 
                ( from p in db.Posts
                        .Include(x => x.Medias)
                        .Include(X => X.Likes)
                        .Include(x => x.Replies)
                        .Include(x => x.Reposts)
                  select new PostViewModel
                  {
                      PostID                = p.PostID,
                      Content               = p.Content,
                      PublisherID           = p.User.Id,
                      PublisherUserName     = p.User.UserName,
                      PublisherTag          = p.User.Tag,
                      PublisherProfilePic   = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}",
                      LikeCount             = p.Likes.Count,
                      ReplyCount            = p.Replies.Count,
                      RepostCount           = p.Reposts.Count,
                      Medias = (  from m in p.Medias
                                  select new MediaViewModel {
                                     MediaID    = m.MediaID,
                                     MIME       = m.MIME,
                                     Path       = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                     IsVideo    = m.MIME.Contains( "video" )
                                  }).ToList()
                  }).ToList();

            return posts;
        }

        public PostDetailViewModel GetOne( int id, string viewerId )
        {
            return (from p in db.Posts
                            .Include( p => p.Medias )
                            .Include( p => p.Likes )
                            .Include( p => p.Replies)
                            .Include( p => p.Reposts)
                    where id == p.PostID
                    select new PostDetailViewModel
                    {
                        PostID                  = p.PostID,
                        Content                 = p.Content,
                        PublisherID             = p.User.Id,
                        PublisherUserName       = p.User.UserName,
                        PublisherTag            = p.User.Tag,
                        PublisherProfilePic     = p.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{p.User.ProfilePic.Name}" : "",
                        IsLiked                 = p.Likes   .FirstOrDefault(l => l.UserID == viewerId) != null,
                        IsReposted              = p.Reposts .FirstOrDefault(l => l.UserID == viewerId) != null,
                        LikeCount               = p.Likes   .Count,
                        ReplyCount              = p.Replies .Count,
                        RepostCount             = p.Reposts .Count,
                        Date                    = p.PostDate,
                        Medias = (from m in p.Medias
                                  select new MediaViewModel{
                                      MediaID   = m.MediaID,
                                      MIME      = m.MIME,
                                      Path      = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                      IsVideo   = m.MIME.Contains("video")
                                  }).ToList(),
                        Replies = (from r in p.Replies.AsQueryable().Include(p=>p.Medias)
                                   select new ReplyViewModel
                                   {
                                       ReplyID              = r.ReplyID,
                                       Content              = r.ContentReplies,
                                       PostID               = r.Post.PostID,
                                       PublisherID          = r.User.Id,
                                       PublisherUserName    = r.User.UserName,
                                       PublisherTag         = r.User.Tag,
                                       PublisherProfilePic  = r.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{ r.User.ProfilePic.Name}" : "",
                                       Date                 = r.ReplyDate,
                                       Medias  = (from mr in r.Medias
                                                  select new MediaViewModel {
                                                    MediaID = mr.MediaID,
                                                    MIME    = mr.MIME,
                                                    Path    = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{mr.Name}",
                                                    IsVideo = mr.MIME.Contains("video")
                                                  }).ToList(),
                                   }).ToList()
                    }).FirstOrDefault();
        }

        public ResponseApiError Update( int id, UPostModel model, ref PostViewModel refModel)
        {
            try
            {
                var err = ValidateExists(id);
                if (err != null)
                    return err;
                
                //err = ValidateU( id, model );
                //if (err != null)
                //    return err;

                var post = db.Posts.Where(u => u.PostID == id)
                    .Include(x => x.Medias)
                    .Include(x => x.Likes)
                    .Include(x => x.Replies)
                    .Include(x => x.Reposts)
                    .Include(x => x.User)
                    .First();

                db.Attach( post );
                post.Content = model.Content;

                if( model.Deleted != null)
                {
                    var medias = (from m in db.PostMedias where model.Deleted.Contains(m.MediaID) select m).ToList();

                    foreach ( var m in medias)
                    {
                        File.Delete( Path.Combine( "static", m.Name ) );

                        post.Medias.Remove(m);
                        db.PostMedias.Remove(m);    
                    }
                }

                if (model.Files != null)
                {
                    var list = new List<PostMedia>();
                    var mediaCore = new MediaCore(db, env, request);
                    err = mediaCore.CreatePostMedia(model.Files, ref list);

                    if (list == null && model.Files?.Count != 0)
                        throw new Exception("Error subiendo media");

                    post.Medias.AddRange(list);
                }

                db.SaveChanges();

                refModel = new PostViewModel
                {
                    Content =       post.Content,
                    PublisherID =   post.User.Id,
                    PostID =        post.PostID,
                    Medias = (from m in post.Medias
                              select new MediaViewModel
                              {
                                  MediaID = m.MediaID,
                                  MIME = m.MIME,
                                  Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                  IsVideo = m.MIME.Contains("video")
                              }).ToList()

                };

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

        public ResponseApiError Delete( int id )
        {
            try
            {
                var err = ValidateExists( id );
                if (err != null)
                    return err;

                var post = db.Posts
                    .Include(p=>p.Medias)
                    .Include(p=>p.Replies)
                    .ThenInclude(r=>r.Medias)
                    .First( p => p.PostID == id );

                foreach (var m in post.Medias)
                {
                    File.Delete(Path.Combine("static", m.Name));
                }

                foreach ( var r in post.Replies)
                {
                    foreach (var m in r.Medias)
                    {
                        File.Delete(Path.Combine("static", m.Name));
                    }
                }

                db.Posts.Remove(post);

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