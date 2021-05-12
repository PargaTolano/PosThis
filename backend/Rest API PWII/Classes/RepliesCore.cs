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
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Reply data not valid, must have text content"
                };

            return null;
        }

        public ResponseApiError ValidateCreation(ReplyViewModel model)
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

        public ResponseApiError ValidateCUReply(ReplyViewModel model)
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
                    Code = (int)HttpStatusCode.NotFound,
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
                    Code = (int)HttpStatusCode.NotFound, 
                    HttpStatusCode = (int)HttpStatusCode.NotFound, 
                    Message = "Requested reply does not exist in database"
                };

            return null;
        }

        public ResponseApiError Create(ReplyViewModel model)
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
                    ContentReplies = model.Content,
                };

                db.Replies.Add(reply);
                db.SaveChanges();

                db.Attach(reply);

                reply.Post = post;
                reply.User = user;

                db.SaveChanges();

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
                    UserID  = r.User.Id,
                 }).ToList();

            return reply;
        }

        public ReplyViewModel GetOne(int id)
        {
            return 
                (from r in db.Replies 
                 where id == r.ReplyID
                 select new ReplyViewModel
                 {
                     ReplyID = r.ReplyID,
                     Content = r.ContentReplies,
                     PostID = r.Post.PostID,
                     UserID = r.User.Id,
                 }).FirstOrDefault();
        }

        public ResponseApiError Update( int id, ReplyViewModel reply )
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

                //TODO MEDIA

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
