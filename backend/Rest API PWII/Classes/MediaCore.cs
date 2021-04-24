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

        public ResponseApiError Validate( MediaViewModel media )
        {
            if ( media.Content == null || media.Content?.Length == 0 )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid data, must have media files"
                };

            return null;
        }

        public ResponseApiError ValidateExists( Media media )
        {
            var res = (from m in db.Medias where m.MediaID == media.MediaID select m).First();

            if (res == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "Media does not exists in database"
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var res = (from m in db.Medias where m.MediaID == id select m).First();

            if ( res == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = ( int ) HttpStatusCode.NotFound,
                    Message = "Media does not exist in database"
                };

            return null;
        }

        public ResponseApiError Create(MediaViewModel model )
        {
            try
            {
                var err = Validate(model);
                if (err != null) 
                    return err;

                var media = new Media {
                    MediaID = model.MediaID,
                    MIME    = model.MIME,
                    Content = model.Content
                };

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
                    Message = ex.Message
                };
            }
        }

        public List<Media> GetAll()
        {
            var media = (from m in db.Medias select m).ToList();

            return media;
        }

        public Media GetOne( int id )
        {
            return db.Medias.FirstOrDefault( m => m.MediaID == id );
        }

        public ResponseApiError Delete( int id )
        {
            try
            {
                var err = ValidateExists( id );
                if (err != null)
                    return err;

                var mediaDb = db.Medias.First(m => m.MediaID == id);

                db.Medias.Remove( mediaDb );

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