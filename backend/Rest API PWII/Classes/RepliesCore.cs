using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Rest_API_PWII.Classes
{
    public class RepliesCore
    {
        private PosThisDbContext db;

        public RepliesCore(PosThisDbContext db)
        {
            this.db = db;
        }

        public ResponseApiError Validate(Reply reply)
        {
            if (reply.Post.Content == null || reply.Post.Content == "")
                return new ResponseApiError{
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Reply data not valid, must have text content"
                };

            return null;
        }

        public ResponseApiError ValidateCreation(CUReplyModel model)
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
                    Message = "UsuarioID does not accept null"
                };

            var user = db.Users.FirstOrDefault(u => u.Id == model.UserID );
            if (user == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found"
                };

            return null;
        }

        public ResponseApiError ValidateCUReply(CUReplyModel model)
        {
            //si el texto esta vacio
            bool textoValido = !string.IsNullOrEmpty(model.Content);
            //y no hay media
            bool mediaValido = model.mediaIDs?.Count > 0;

            if (textoValido || mediaValido)
                return null;

            //hay error
            return new ResponseApiError
            {
                Code = (int)HttpStatusCode.BadRequest,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Reply data not valid, must contain text or media"
            };
        }

        public ResponseApiError ValidateExists(Reply reply)
        {
            var res = (from r in db.Replies where r.ReplyID == reply.ReplyID select r).First();

            if (res == null)
                return new ResponseApiError{
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Reply does not exist in database"
                };

            return null;
        }

        public ResponseApiError ValidateExists(int id)
        {
            var res = (from r in db.Replies where r.ReplyID == id select r).First();

            if (res == null)
                return new ResponseApiError { 
                    Code = 2, 
                    HttpStatusCode = (int)HttpStatusCode.NotFound, 
                    Message = "Requested reply does not exist in database"
                };

            return null;
        }

        public ResponseApiError Create(CUReplyModel model)
        {
            try
            {
                var err = ValidateCUReply(model);
                if ( err != null )
                    return err;

                err = ValidateCreation( model );
                if ( err != null )
                    return err;

                var post = db.Posts.First( p => p.PostID == model.PostID );

                var user = db.Users.First( u => u.Id == model.UserID );

                var reply = new Reply {
                    PostID = post.PostID,
                    Post = post,
                    UserID = user.Id,
                    User = user,
                    ContentReplies = model.Content,
                };

                var replyEntry = db.Replies.Add(reply);

                var replyDb = replyEntry.Entity;

                if ( model.mediaIDs?.Count > 0 )
                {
                    var medias = db.Medias.Where( m => model.mediaIDs.Contains(m.MediaID) ).ToList();

                    var mediaReplies = new List<MediaReply>();

                    foreach (var m in medias)
                    {
                        var mediaReply = new MediaReply
                        {
                            MediaID = m.MediaID,
                            Media = m,
                            ReplyID = replyDb.ReplyID,
                            Reply = replyDb
                        };

                        mediaReplies.Add(mediaReply);
                    }

                    replyDb.MediaReplies = mediaReplies;
                }

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError{
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = ex.Message
                };
            }
        }

        public List<Reply> GetAll()
        {
            List<Reply> reply = (from r in db.Replies select r).ToList();

            return reply;
        }

        public Reply GetOne(int id)
        {
            return 
                (from r in db.Replies where id == r.ReplyID select r)
                    .FirstOrDefault();
        }

        public ResponseApiError Update( int id, CUReplyModel reply )
        {
            try
            {
                var err = ValidateCUReply( reply );
                if (err != null)
                    return err;

                err = ValidateExists(id);
                if (err != null)
                    return err;

                var replyDb = db.Replies.First( r => r.ReplyID == id );

                replyDb.ContentReplies = reply.Content;
                
                if( reply.mediaIDs?.Count > 0)
                {
                    var medias = db.Medias.Where(  m => reply.mediaIDs.Contains(m.MediaID) ).ToList();

                    var mediaReplies = new List<MediaReply>();

                    foreach ( var m in medias)
                    {
                        var mediaReply = new MediaReply
                        {
                            MediaID = m.MediaID,
                            Media = m,
                            ReplyID = replyDb.ReplyID,
                            Reply = replyDb
                        };

                        mediaReplies.Add( mediaReply );
                    }

                    replyDb.MediaReplies = mediaReplies;
                }

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseApiError Delete(int id)
        {
            try
            {
                ResponseApiError err = ValidateExists(id);
                if (err != null)
                    return err;

                Reply replyDb = db.Replies.First(r => r.ReplyID == id);

                db.Replies.Remove(replyDb);

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
