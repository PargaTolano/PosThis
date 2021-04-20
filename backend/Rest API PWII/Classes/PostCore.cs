using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class PostCore
    {
        private PosThisDbContext db;

        public PostCore( PosThisDbContext db )
        {
            this.db = db;
        }

        public ResponseApiError Validate( Post post )
        {
            if ( post.Texto == null && post.MediaPosts.Count == 0 )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Los datos del post no son validos, debe tener contenido de texto y/o media"
                };

            return null;
        }

        public ResponseApiError ValidateCU(CUPostModel createPostModel)
        {
            bool textoValido = !string.IsNullOrEmpty(createPostModel.Texto);
            bool mediaValido = createPostModel.mediaIDs?.Count > 0;

            if (textoValido || mediaValido)
                return null;

            return new ResponseApiError
            {
                Code = 400,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Post no valido, debe de contener texto o almenos un archivo multimedia"
            };
        }

        public ResponseApiError ValidateExists( Post post )
        {
            var res = ( from p in db.Posts where p.PostID == post.PostID select p ).First();

            if ( res == null )
                return new ResponseApiError {
                    Code = 2, HttpStatusCode = (int) HttpStatusCode.NotFound,
                    Message = "El post no existe en la base de datos" 
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var res = (from p in db.Posts where p.PostID == id select p).First();

            if ( res == null )
                return new ResponseApiError {
                    Code = 2, HttpStatusCode = (int) HttpStatusCode.NotFound,
                    Message = "El post no existe en la base de datos" 
                };


            return null;
        }

        public ResponseApiError Create(CUPostModel createPostModel )
        {
            try
            {
                var err = ValidateCU(createPostModel);
                if (err == null)
                    return err;

                var post = new Post { 
                    UsuarioID = createPostModel.UsuarioID,
                    Texto = createPostModel.Texto,
                };

                var entry = db.Posts.Add( post );

                var postDb = entry.Entity;

                postDb.FechaPublicacion = DateTime.Now;

                if (createPostModel.mediaIDs.Count > 0)
                {
                    var medias = db.Medias.Where(m => createPostModel.mediaIDs.Contains(m.MediaID)).ToList();
                    var mediaPosts = new List<MediaPost>();

                    foreach ( var m in medias)
                    {
                        var mediaPost = new MediaPost
                        {
                            MediaID = m.MediaID,
                            Media = m,
                            Post = postDb,
                            PostID = postDb.PostID
                        };

                        db.MediaPosts.Add( mediaPost );

                        mediaPosts.Add( mediaPost );
                    }

                    postDb.MediaPosts = mediaPosts;
                }

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {

                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.InnerException.Message
                };
            }
        }

        public List<Post> GetAll()
        {
            List<Post> usuarios = ( from p in db.Posts select p ).ToList();

            return usuarios;
        }

        public Post GetOne( int id )
        {
            return db.Posts.First(p => p.PostID == id);
        }

        public ResponseApiError Update( int id, CUPostModel post )
        {
            try
            {
                ResponseApiError err = ValidateCU(post);
                if (err != null)
                    return err;

                err = ValidateExists(id);
                if (err != null)
                    return err;

                Post postDb = db.Posts.First(u => u.PostID == id);

                postDb.Texto = post.Texto;

                if ( postDb.MediaPosts?.Count > 0)
                {
                    foreach( var mp in postDb.MediaPosts)
                    {
                        db.Medias.Remove( mp.Media );
                        db.MediaPosts.Remove( mp );
                    }

                    postDb.MediaPosts = null;
                }

                if ( post.mediaIDs?.Count > 0 )
                {
                    var medias = db.Medias.Where(m => post.mediaIDs.Contains(m.MediaID)).ToList();
                    var mediaPosts = new List<MediaPost>();

                    foreach (var m in medias)
                    {
                        var mediaPost = new MediaPost
                        {
                            MediaID = m.MediaID,
                            Media = m,
                            Post = postDb,
                            PostID = postDb.PostID
                        };

                        db.MediaPosts.Add(mediaPost);

                        mediaPosts.Add(mediaPost);
                    }

                    postDb.MediaPosts = mediaPosts;
                }

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }

        public ResponseApiError Delete( int id )
        {
            try
            {
                ResponseApiError err = ValidateExists( id );
                if (err != null)
                    return err;

                Post postDb = db.Posts.First( p => p.PostID == id );

                db.Posts.Remove( postDb );

                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno del servidor"
                };
            }
        }
    }
}