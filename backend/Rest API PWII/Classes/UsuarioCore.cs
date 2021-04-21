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

        public ResponseApiError Validate( User usuario )
        {
            if ( usuario.UserName == null || usuario.Tag == null || usuario.Email == null )
                return new ResponseApiError { 
                    Code = 1,
                    HttpStatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Los datos del usuario no son validos" 
                };

            return null;
        }

        public ResponseApiError ValidateUpdate( UserViewModel model )
        {
            if( 
                model.Nombre == null &&
                model.Tag == null &&
                model.Email == null &&
                model.FotoPerfilID == null &&
                model.FechaNacimiento == null 
                )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Se debe modificar almenos un campo"
                };

            return null;
        }

        public ResponseApiError ValidateNewPassword( User usuario)
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

        public ResponseApiError ValidateExists( User usuario )
        {
            var res = (from u in db.Users where u.Id == usuario.Id select u).First();

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
            var res = (from u in db.Users where u.Id == id select u).First();

            if (res == null)
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "El usuario no existe en la base de datos"
                };

            return null;
        }
        
        public ResponseApiError Create( User usuario )
        {
            try
            {
                ResponseApiError err = Validate(usuario);
                if (err != null)/*estaba == */
                    return err;

                db.Users.Add( usuario );
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
            List<UserViewModel> usuarios = 
                (from u 
                 in db.Users 
                 select new UserViewModel {
                     Id = u.Id,
                     Nombre = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     FechaNacimiento = u.FechaNacimiento,
                     FotoPerfilID = u.FotoPerfilMediaID
                 }).DefaultIfEmpty().ToList();
            return usuarios;
        }

        public UserViewModel GetOne( string id )
        {
            return (from u
                    in db.Users
                    where u.Id == id
                    select new UserViewModel
                    {
                        Id = u.Id,
                        Nombre = u.UserName,
                        Tag = u.Tag,
                        Email = u.Email,
                        FechaNacimiento = u.FechaNacimiento,
                        FotoPerfilID = u.FotoPerfilMediaID
                    }).FirstOrDefault();
        }

        public ResponseApiError Update( string id, UserViewModel usuario )
        {
            try
            {
                ResponseApiError err = ValidateUpdate( usuario );
                if (err != null)
                    return err;

                err = ValidateExists( id );
                if (err != null)
                    return err;

                User usuarioDb = db.Users.First( u => u.Id == id );

                usuarioDb.UserName  = usuario.Nombre != null ? usuario.Nombre : usuarioDb.UserName;
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

                User usuarioDb = db.Users.First( u => u.Id == id );

                db.Users.Remove( usuarioDb );

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
