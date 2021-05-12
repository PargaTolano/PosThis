using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;


namespace Rest_API_PWII.Classes
{
    public enum UserMediaType
    {
        ProfilePic,
        CoverPic
    }

    public class MediaCore
    {
        private PosThisDbContext db;
        private IHostingEnvironment env;
        private HttpRequest request;

        public MediaCore(PosThisDbContext db, IHostingEnvironment env, HttpRequest request)
        {
            this.db = db;
            this.env = env;
            this.request = request;
        }

        public ResponseApiError Validate( IFormFile files )
        {
            if (files.Length == 0)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid data, file must not be empty"
                };

            return null;
        }

        public ResponseApiError Validate( List<IFormFile> files )
        {
            if (files?.Count == 0)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid data, must have media files"
                };

            return null;
        }

        public ResponseApiError ValidateUserMediaExists( int id )
        {
            var um = db.UserMedias.FirstOrDefault( x => x.MediaID == id );

            if ( um == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User picture must exist"
                };

            return null;
        }

        public ResponseApiError ValidatePostMediaExists( int id )
        {
            var um = db.PostMedias.FirstOrDefault(x => x.MediaID == id);

            if (um == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post picture or video must exist"
                };

            return null;
        }

        public ResponseApiError ValidateReplyMediaExists( int id )
        {
            var um = db.ReplyMedias.FirstOrDefault(x => x.MediaID == id);

            if (um == null)
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Reply picture or video picture must exist"
                };

            return null;
        }

        public ResponseApiError CreateUserMedia( IFormFile file, ref UserMedia media )
        {
            var err = Validate(file);
            if (err != null)
                return err;

            var fileName = Guid.NewGuid() + file.FileName;

            var path = Path.Combine("static", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            media = new UserMedia
            {
                MIME = file.ContentType,
                Name = fileName
            };

            db.UserMedias.Add(media);
            db.SaveChanges();

            return null;
        }

        public ResponseApiError CreatePostMedia( List<IFormFile> files, ref List<PostMedia> list )
        {
            var err = Validate(files);
            if (err != null)
                return err;

            list = new List<PostMedia>();


            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                var fileName = Guid.NewGuid() + file.FileName;

                var path = Path.Combine("static", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var media = new PostMedia
                {
                    MIME = file.ContentType,
                    Name = fileName
                };

                db.PostMedias.Add(media);
                db.SaveChanges();
                list.Add(media);
            }

            return null;
        }

        public ResponseApiError CreateReplyMedia( List<IFormFile> files, ref List<ReplyMedia> list )
        {
            var err = Validate(files);
            if (err != null)
                return err;

            list = new List<ReplyMedia>();


            foreach (var file in files)
            {
                if (file.Length == 0)
                    continue;

                var fileName = Guid.NewGuid() + file.FileName;

                var path = Path.Combine("static", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var media = new ReplyMedia
                {
                    MIME = file.ContentType,
                    Name = fileName
                };

                db.ReplyMedias.Add(media);
                db.SaveChanges();
                list.Add(media);
            }

            return null;
        }

        public ResponseApiError DeleteUserMedia( int id )
        {
            var err = ValidateUserMediaExists( id );
            if (err != null)
                return err;

            var um = db.UserMedias.First( x => x.MediaID == id );

            var path = Path.Combine( "static", um.Name );

            File.Delete(path);

            db.UserMedias.Remove(um);
            db.SaveChanges();

            return null;
        }

        public ResponseApiError DeletePostMedia( int id )
        {
            var err = ValidatePostMediaExists( id );
            if ( err != null )
                return err;

            var pm = db.PostMedias.First( x => x.MediaID == id );

            var path = Path.Combine( "static", pm.Name );

            File.Delete(path);

            db.PostMedias.Remove( pm );
            db.SaveChanges();

            return null;
        }

        public ResponseApiError DeleteReplyMedia( int id )
        {
            var err = ValidateReplyMediaExists( id );
            if ( err != null )
                return err;

            var rm = db.ReplyMedias.First( x => x.MediaID == id );

            var path = Path.Combine( "static", rm.Name );

            File.Delete( path );

            db.ReplyMedias.Remove( rm );
            db.SaveChanges();

            return null;
        }
    }

}