using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

        public ResponseApiError Create( Post post )
        {
            try
            {
                ResponseApiError err = Validate(post);
                if (err == null)
                    return err;

                db.Posts.Add(post);
                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno del servidor"
                };
            }
        }

        public List<Post> GetAll()
        {
            List<Post> usuarios = (from p in db.Posts select p).ToList();

            return usuarios;
        }

        public Post GetOne( int id )
        {
            return db.Posts.First(p => p.PostID == id);
        }

        public ResponseApiError Update( int id, Post post )
        {
            try
            {
                ResponseApiError err = Validate(post);
                if (err != null)
                    return err;

                err = ValidateExists(id);
                if (err != null)
                    return err;

                Post postDb = db.Posts.First(u => u.PostID == id);

                postDb.Texto = post.Texto;
                postDb.MediaPosts = post.MediaPosts;

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError
                {
                    Code = 3,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno del servidor"
                };
            }
        }

        public ResponseApiError Delete( int id )
        {
            try
            {
                ResponseApiError err = ValidateExists(id);
                if (err != null)
                    return err;

                Usuario usuarioDb = db.Usuarios.First(u => u.UsuarioID == id.ToString());

                db.Usuarios.Remove(usuarioDb);

                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
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