using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;


namespace Rest_API_PWII.Classes
{
    public class MediaCore
    {
        private PosThisDbContext db;

        public MediaCore(PosThisDbContext db)
        {
            this.db = db;
        }

        public ResponseApiError Validate( Media media )
        {
            if ( media.Contenido != null && media.Contenido.Length != 0 )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Los datos del post no son validos, debe tener contenido de texto y/o media"
                };

            return null;
        }

        public ResponseApiError ValidateExists( Media media )
        {
            var res = (from m in db.Medias where m.MediaID == media.MediaID select m).First();

            if (res == null)
                return new ResponseApiError
                {
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "El post no existe en la base de datos"
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var res = (from m in db.Medias where m.MediaID == id select m).First();

            if ( res == null )
                return new ResponseApiError
                {
                    Code = 2,
                    HttpStatusCode = ( int ) HttpStatusCode.NotFound,
                    Message = "El post no existe en la base de datos"
                };


            return null;
        }

        public ResponseApiError Create( Media media )
        {
            try
            {
                ResponseApiError err = Validate( media );
                if (err != null) //Diferente para crearlo
                    return err;

                db.Medias.Add( media );
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

        public List<Media> GetAll()
        {
            List<Media> media = (from m in db.Medias select m).ToList();

            return media;
        }

        public Media GetOne( int id )
        {
            return db.Medias.First( m => m.MediaID == id );
        }

        public ResponseApiError Delete( int id )
        {
            try
            {
                ResponseApiError err = ValidateExists( id );
                if (err != null)
                    return err;

                Usuario usuarioDb = db.Usuarios.First(u => Int32.Parse(u.UsuarioID) == id);

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