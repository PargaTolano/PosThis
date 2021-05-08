using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public ResponseApiError Validate(List<IFormFile> files )
        {
            if ( files?.Count == 0 )
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

        public ResponseApiError Create( 
            List<IFormFile> files,
            string Scheme,
            string Host,
            string PathBase,
            ref List<MediaViewModel> list )
        {
            try
            {
                var err = Validate(files);
                if (err != null) 
                    return err;

                list = new List<MediaViewModel>();


                foreach( var file in files)
                {
                    if (file.Length == 0)
                        continue;

                    var fileName = Guid.NewGuid() + file.FileName;

                    var path = Path.Combine("static", fileName);

                    using ( var fileStream = new FileStream( path, FileMode.Create ) )
                    {
                        file.CopyTo( fileStream );
                    }

                    var media = new Media
                    {
                        MIME = file.ContentType,
                        Name = fileName
                    };

                    var entry = db.Medias.Add( media );
                    var mediaDb = entry.Entity;

                    var viewModel = new MediaViewModel
                    {
                        MediaID = mediaDb.MediaID,
                        MIME    = mediaDb.MIME,
                        Path    = $"{Scheme}://{Host}{PathBase}/static/{mediaDb.Name}",
                        IsVideo = mediaDb.MIME.Contains( "video" )
                    };

                    list.Add( viewModel );
                }
                
                db.SaveChanges();

                return null;
            }
            catch ( Exception ex )
            {
                list = null;
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.InternalServerError,
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

        public MediaViewModel GetOne( int id , string Scheme, string Host, string PathBase )
        {
            var MediaViewModel = (
                from m in db.Medias
                where m.MediaID == id
                select new MediaViewModel
                {
                    MediaID = m.MediaID,
                    MIME = m.MIME,
                    Path =$"{Scheme}://{Host}{PathBase}/static/{m.Name}",
                    IsVideo = m.MIME.Contains("video")
                }).FirstOrDefault();

            return MediaViewModel;
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