using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rest_API_PWII.Classes
{
    public class RepliesCore
    {
        private PosThisDbContext db;
        private IHostingEnvironment env;
        private HttpRequest request;

        public RepliesCore( PosThisDbContext db, IHostingEnvironment env, HttpRequest request )
        {
            this.db = db;
            this.env = env;
            this.request = request;
        }

        public ResponseApiError Validate( Reply reply )
        {
            if (reply.Post.Content == null || reply.Post.Content == "")
                return new ResponseApiError{
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Reply data not valid, must have text content"
                };

            return null;
        }

        public ResponseApiError ValidateC ( CReplyModel model )
        {
            if (model.PostID == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "PostID does not accept null"
                };

            var post = db.Posts.FirstOrDefault(p => p.PostID == model.PostID);
            if (post == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Post not found"
                };

            if (model.UserID == null)
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
                    Message = "User not found"
                };

            bool textoValido = !string.IsNullOrEmpty(model.Content);
            bool mediaValido = model.Files?.Count > 0;

            if (textoValido || mediaValido)
                return null;

            return new ResponseApiError
            {
                Code = (int)HttpStatusCode.BadRequest,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Reply data not valid, must contain text or media"
            };
        }

        public ResponseApiError ValidateUReply( UReplyModel model )
        {
            bool textoValido = !string.IsNullOrEmpty(model.Content);
            bool mediaValido = model.Files?.Count > 0;

            if (textoValido || mediaValido)
                return null;

            return new ResponseApiError
            {
                Code = (int)HttpStatusCode.BadRequest,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Reply data not valid, must contain text or media"
            };
        }

        public ResponseApiError ValidateExists( Reply reply )
        {
            var res = (from r in db.Replies where r.ReplyID == reply.ReplyID select r).First();

            if (res == null)
                return new ResponseApiError{
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Reply does not exist in database"
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var res = (from r in db.Replies where r.ReplyID == id select r).FirstOrDefault();

            if (res == null)
                return new ResponseApiError { 
                    Code = (int)HttpStatusCode.NotFound, 
                    HttpStatusCode = (int)HttpStatusCode.NotFound, 
                    Message = "Requested reply does not exist in database"
                };

            return null;
        }

        public ResponseApiError Create( CReplyModel model, ref List<ReplyViewModel> replyViewModels )
        {
            try
            {
                var err = ValidateC(model);
                if ( err != null )
                    return err;

                var post = db.Posts.First( p => p.PostID == model.PostID );
                var user = db.Users.First( u => u.Id == model.UserID );
                var reply = new Reply {
                    User = user,
                    Post = post,
                    ContentReplies = model.Content,
                    ReplyDate   = DateTime.Now,
                };

                var list        = new List<ReplyMedia>();
                var mediaCore   = new MediaCore(db, env, request);
                if (model.Files != null)
                {
                    err = mediaCore.CreateReplyMedia(model.Files, ref list);
                    if (err != null)
                        return err;
                }

                db.Replies.Add(reply);
                db.SaveChanges();

                reply.Medias = list;

                db.SaveChanges();

                replyViewModels = (from r in db.Replies
                                                .Include(r=>r.User)
                                                .Include(r => r.Medias)
                                   where r.PostID == model.PostID 
                                   select new ReplyViewModel {
                                       ReplyID = r.ReplyID,
                                       Content = r.ContentReplies,
                                       PostID = r.Post.PostID,
                                       PublisherID = r.User.Id,
                                       PublisherUserName = r.User.UserName,
                                       PublisherTag = r.User.Tag,
                                       PublisherProfilePic = r.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{ r.User.ProfilePic.Name}" : "",
                                       Date = r.ReplyDate,
                                       Medias = (from mr in r.Medias
                                                 select new MediaViewModel
                                                 {
                                                     MediaID = mr.MediaID,
                                                     MIME = mr.MIME,
                                                     Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{mr.Name}",
                                                     IsVideo = mr.MIME.Contains("video")
                                                 }).ToList(),
                                   }).ToList();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError{
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message
                };
            }
        }

        public List<ReplyViewModel> GetAll()
        {
            var reply = 
                (from r in db.Replies
                 select new ReplyViewModel 
                 {
                    ReplyID = r.ReplyID,
                    Content = r.ContentReplies,
                    PostID  = r.Post.PostID,
                    PublisherID  = r.User.Id,
                 }).ToList();

            return reply;
        }

        public ReplyViewModel GetOne( int id )
        {
            return 
                (from r in db.Replies 
                 where id == r.ReplyID
                 select new ReplyViewModel
                 {
                     ReplyID = r.ReplyID,
                     Content = r.ContentReplies,
                     PostID = r.Post.PostID,
                     PublisherID = r.User.Id,
                 }).FirstOrDefault();
        }

        public ResponseApiError Update( int id, UReplyModel model, ref ReplyViewModel refModel)
        {
            try
            {
               var err = ValidateExists(id);
                if (err != null)
                    return err;

                var reply = (from r in db.Replies.Include(r=>r.Medias) where r.ReplyID == id select r).First();
                
                db.Attach(reply);
                reply.ContentReplies = model.Content;

                if (model.Deleted != null)
                {
                    var medias = (from m in db.ReplyMedias where model.Deleted.Contains(m.MediaID) select m).ToList();

                    foreach (var m in medias)
                    {
                        File.Delete(Path.Combine("static", m.Name));

                        reply.Medias.Remove(m);
                        db.ReplyMedias.Remove(m);
                    }
                }

                if (model.Files != null)
                {
                    var list = new List<ReplyMedia>();
                    var mediaCore = new MediaCore(db, env, request);
                    err = mediaCore.CreateReplyMedia(model.Files, ref list);

                    if (list == null && model.Files?.Count != 0)
                        throw new Exception("Error subiendo media");

                    reply.Medias.AddRange(list);
                }

                db.SaveChanges();

                refModel =(from r in db.Replies.Include(r=>r.User).Include(r=>r.Medias)
                           where r.ReplyID == id
                           select new ReplyViewModel{
                           ReplyID              = r.ReplyID,
                           Content              = r.ContentReplies,
                           PostID               = r.Post.PostID,
                           PublisherID          = r.User.Id,
                           PublisherUserName    = r.User.UserName,
                           PublisherTag         = r.User.Tag,
                           PublisherProfilePic  = r.User.ProfilePic != null ? $"{request.Scheme}://{request.Host}{request.PathBase}/static/{ r.User.ProfilePic.Name}" : "",
                           Date                 = r.ReplyDate,
                           Medias      = (from m in reply.Medias
                                           select new MediaViewModel
                                           {
                                               MediaID = m.MediaID,
                                               MIME = m.MIME,
                                               Path = $"{request.Scheme}://{request.Host}{request.PathBase}/static/{m.Name}",
                                               IsVideo = m.MIME.Contains("video")
                                           }).ToList()
                            }).First();

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

        public ResponseApiError Delete(int id)
        {
            try
            {
                var err = ValidateExists(id);
                if (err != null)
                    return err;

                var reply = db.Replies
                    .Include(r => r.Medias)
                    .First(r => r.ReplyID == id);

                foreach (var m in reply.Medias)
                {
                    File.Delete(Path.Combine("static", m.Name));
                }

                db.Replies.Remove(reply);

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
    }
}
