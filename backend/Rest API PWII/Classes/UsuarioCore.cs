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

        public UsuarioCore( PosThisDbContext db)
        {
            this.db = db;
        }

        public ResponseApiError Validate( Usuario usuario)
        {
            if (usuario.Nombre == null || usuario.Tag == null || usuario.Correo == null)
                return new ResponseApiError { 
                    Code = 1,
                    HttpStatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "Los datos del usuario no son validos" 
                };

            return null;
            
        }

        public ResponseApiError ValidateExists( Usuario usuario)
        {
            var res = (from u in db.Usuarios where u.UsuarioID == usuario.UsuarioID select u).First();

            if (res == null)
                return new ResponseApiError { Code = 2, HttpStatusCode = (int)HttpStatusCode.NotFound, Message = "El usuario no existe en la base de datos" };


            return null;
        }

        public ResponseApiError ValidateExists(int id)
        {
            var res = (from u in db.Usuarios where u.UsuarioID == id select u).First();

            if (res == null)
                return new ResponseApiError { Code = 2, HttpStatusCode = (int)HttpStatusCode.NotFound, Message = "El usuario no existe en la base de datos" };


            return null;
        }
        
        public ResponseApiError Create(Usuario usuario)
        {
            try
            {
                ResponseApiError err = Validate(usuario);
                if (err == null)
                    return err;

                db.Usuarios.Add(usuario);
                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return new ResponseApiError { 
                    Code = 3, 
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "Error interno del servidor" 
                };
            }
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = (from u in db.Usuarios select u).ToList();

            return usuarios;
        }

        public Usuario GetOne(int id)
        {
            return db.Usuarios.First(u => u.UsuarioID == id);
        }

        public ResponseApiError Update(int id, Usuario usuario)
        {
            try
            {
                ResponseApiError err = Validate(usuario);
                if (err != null)
                    return err;

                err = ValidateExists(id);
                if (err != null)
                    return err;

                Usuario usuarioDb = db.Usuarios.First(u => u.UsuarioID == id);

                usuarioDb.Nombre = usuario.Nombre != null ? usuario.Nombre : usuarioDb.Nombre;
                usuarioDb.Tag = usuario.Tag != null ? usuario.Tag : usuarioDb.Tag;
                usuarioDb.Correo = usuario.Correo != null ? usuario.Tag : usuarioDb.Tag;
                usuarioDb.Contrasena = usuario.Contrasena != null ? usuario.Contrasena : usuarioDb.Contrasena;

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

                Usuario usuarioDb = db.Usuarios.First( u => u.UsuarioID == id );

                db.Usuarios.Remove( usuarioDb );

                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }  
    }
}
