using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class UsuarioCore
    {
        private PosThisDbContext db;

        public UsuarioCore( PosThisDbContext db )
        {
            this.db = db;
        }

        public ResponseApiError Validate( Usuario usuario )
        {
            if ( usuario.UserName == null || usuario.Tag == null || usuario.Email == null )
                return new ResponseApiError { 
                    Code = 1,
                    HttpStatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Los datos del usuario no son validos" 
                };

            return null;
        }

        public ResponseApiError ValidateUpdate( Usuario usuario )
        {
            if( usuario.Email == null )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Los datos del usuario no son validos"
                };

            return null;
        }

        public ResponseApiError ValidateNewPassword( Usuario usuario)
        {
            if ( usuario.PasswordHash == null )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Los datos del usuario no son validos"
                };

            return null;
        }

        public ResponseApiError ValidateExists( Usuario usuario )
        {
            var res = (from u in db.Usuarios where u.Id == usuario.Id select u).First();

            if (res == null)
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound, 
                    Message = "El usuario no existe en la base de datos" 
                };

            return null;
        }

        public ResponseApiError ValidateExists( string id )
        {
            var res = (from u in db.Usuarios where u.Id == id select u).First();

            if (res == null)
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "El usuario no existe en la base de datos"
                };

            return null;
        }
        
        public ResponseApiError Create( Usuario usuario )
        {
            try
            {
                ResponseApiError err = Validate( usuario );
                if ( err == null )
                    return err;

                db.Usuarios.Add( usuario );
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

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = ( from u in db.Usuarios select u ).ToList();
            return usuarios;
        }

        public Usuario GetOne( string id )
        {
            return db.Usuarios.First( u => u.Id == id );
        }

        public ResponseApiError Update( string id, Usuario usuario )
        {
            try
            {
                ResponseApiError err = ValidateUpdate( usuario );
                if (err != null)
                    return err;

                err = ValidateExists( id );
                if (err != null)
                    return err;

                Usuario usuarioDb = db.Usuarios.First( u => u.Id == id );

                usuarioDb.UserName  = usuario.UserName != null ? usuario.UserName : usuarioDb.UserName;
                usuarioDb.Tag       = usuario.Tag != null ? usuario.Tag : usuarioDb.Tag;
                usuarioDb.Email     = usuario.Email != null ? usuario.Email : usuarioDb.Email;

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

        public ResponseApiError Delete( string id )
        {
            try
            {
                var err = ValidateExists( id );
                if (err != null)
                    return err;

                Usuario usuarioDb = db.Usuarios.First( u => u.Id == id );

                db.Usuarios.Remove( usuarioDb );

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
