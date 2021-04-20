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
            if (reply.Post.Texto == null || reply.Post.Texto == "")
                return new ResponseApiError{
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Los datos de la respuesta no son validos, debe tener contenido de texto"
                };

            return null;
        }

        public ResponseApiError ValidateUpdateReply(UpdateReplyModel reply)
        {
            //si el texto esta vacio
            bool textoValido = string.IsNullOrEmpty(reply.Texto);
            //y no hay media
            bool mediaValido = reply.mediaIDs.Count() > 0;

            if (textoValido || mediaValido)
                return null;

            //hay error
            return new ResponseApiError
            {
                Code = 1,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Los datos de la respuesta no son validos, debe tener contenido de texto"
            };
        }

        public ResponseApiError ValidateExists(Reply reply)
        {
            var res = (from r in db.Replies where r.ReplyID == reply.ReplyID select r).First();

            if (res == null)
                return new ResponseApiError{
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "El reply no existe en la base de datos"
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
                    Message = "El reply específico no existe en la base de datos" 
                };

            return null;
        }

        public ResponseApiError Create(Reply reply)
        {
            try
            {
                ResponseApiError err = Validate(reply);
                if (err != null) 
                    return err;

                db.Replies.Add(reply);
                db.SaveChanges();

                return null;

            }
            catch (Exception ex)
            {
                return new ResponseApiError{
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Error interno del servidor"
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
            return db.Replies.First(p => p.PostID == id);
        }

        public ResponseApiError Update( int id, UpdateReplyModel reply )
        {
            try
            {
                ResponseApiError err = ValidateUpdateReply( reply );
                if (err != null)
                    return err;

                err = ValidateExists(id);
                if (err != null)
                    return err;

                Reply replyDb = db.Replies.First(r => r.ReplyID == id);

                replyDb.Texto = reply.Texto;
                
                if( reply.mediaIDs.Count() > 0)
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
