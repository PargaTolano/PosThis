using System;
using System.Collections.Generic;
using System.IO;
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
            if ( post.Content == null && post.MediaPosts.Count == 0 )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Post data not valid, must have text content and/or media"
                };

            return null;
        }

        public ResponseApiError ValidateCU(CPostModel createPostModel)
        {
            bool textoValido = !string.IsNullOrEmpty(createPostModel.Content);
            bool mediaValido = createPostModel.Files?.Count > 0;

            if (textoValido || mediaValido)
                return null;

            return new ResponseApiError
            {
                Code = 400,
                HttpStatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Invalid post, must have text centent and at least one media file"
            };
        }

        public ResponseApiError ValidateExists( Post post )
        {
            var res = ( from p in db.Posts where p.PostID == post.PostID select p ).FirstOrDefault();

            if ( res == null )
                return new ResponseApiError {
                    Code = (int)HttpStatusCode.NotFound,
                    HttpStatusCode = (int) HttpStatusCode.NotFound,
                    Message = "Post does not exist in database" 
                };

            return null;
        }

        public ResponseApiError ValidateExists( int id )
        {
            var res = (from p in db.Posts where p.PostID == id select p).FirstOrDefault();

            if ( res == null )
                return new ResponseApiError {
                    Code = (int)HttpStatusCode.NotFound, 
                    HttpStatusCode = (int) HttpStatusCode.NotFound,
                    Message = "Post does not exist in database"
                };


            return null;
        }

        public ResponseApiError Create(
            CPostModel model,
            string Scheme,
            string Host,
            string PathBase)
        {
            try
            {
                var err = ValidateCU( model );
                if (err != null)
                    return err;

                var userDb = db.Users.FirstOrDefault( u => u.Id == model.UserID );
                if (userDb == null)
                    return new ResponseApiError
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = "User not found"
                    };

                var list = new List<MediaViewModel>();

                var mediaCore = new MediaCore( db );
                err = mediaCore.Create( model.Files, Scheme, Host, PathBase, ref list);

                
                if (list == null && model.Files.Count != 0)
                    throw new Exception("Error subiendo media");


                var post = new Post
                {
                    UserID = userDb.Id,
                    User = userDb,
                    Content = model.Content,
                    PostDate = DateTime.Now
                };

                var entry = db.Posts.Add(post);
                var postDb = entry.Entity;

                if (model.Files.Count != 0) { 
                
                    var ids = (from mvm in list select mvm.MediaID).ToList();

                    foreach (var id in ids)
                    {
                        var mediaPost = new MediaPost
                        {
                            MediaID = id,
                            PostID = postDb.PostID,
                            Media = db.Medias.FirstOrDefault(x => x.MediaID == id),
                            Post = postDb
                        };

                        db.MediaPosts.Add( mediaPost );
                    }
                }
                
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

        public List<PostViewModel> GetAll( string  Scheme, string Host, string PathBase )
        {
            var posts = 
                ( from p in db.Posts 
                  join u in db.Users
                  on p.UserID equals u.Id
                  select new PostViewModel
                  {
                      PostID    = p.PostID,
                      Content   = p.Content,
                      UserID    = u.Id,
                      UserName  = u.UserName,
                      UserTag   = u.Tag,
                      UserProfilePicID = u.ProfilePhotoMediaID,
                      Medias = (  from mp in db.MediaPosts
                                  join po in db.Posts
                                  on mp.PostID equals po.PostID
                                  join m in db.Medias
                                  on mp.MediaID equals m.MediaID
                                  where po.PostID == p.PostID
                                  select new MediaViewModel {
                                     MediaID = m.MediaID,
                                     MIME = m.MIME,
                                     Path = $"{Scheme}://{Host}{PathBase}/static/{m.Name}",
                                     IsVideo = m.MIME.Contains( "video" )
                                  }).ToList()
                  }).ToList();

            return posts;
        }

        public PostViewModel GetOne( int id, string Scheme, string Host, string PathBase)
        {
            return (from p in db.Posts
                    join u in db.Users
                    on p.UserID equals u.Id
                    where id == p.PostID
                    select new PostViewModel
                    {
                        PostID = p.PostID,
                        Content = p.Content,
                        UserID = u.Id,
                        UserName = u.UserName,
                        UserTag = u.Tag,
                        UserProfilePicID = u.ProfilePhotoMediaID,
                        Medias = (from mp in db.MediaPosts
                                  join po in db.Posts
                                  on mp.PostID equals po.PostID
                                  join m in db.Medias
                                  on mp.MediaID equals m.MediaID
                                  where po.PostID == p.PostID
                                  select new MediaViewModel
                                  {
                                      MediaID = m.MediaID,
                                      MIME = m.MIME,
                                      Path = $"{Scheme}://{Host}{PathBase}/static/{m.Name}",
                                      IsVideo = m.MIME.Contains("video")
                                  }).ToList()
                    }).FirstOrDefault();
        }

        public ResponseApiError Update( int id, CPostModel post )
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

                postDb.Content = post.Content;

                if ( postDb.MediaPosts?.Count > 0)
                {
                    foreach( var mp in postDb.MediaPosts)
                    {
                        db.Medias.Remove( mp.Media );
                        db.MediaPosts.Remove( mp );
                    }

                    postDb.MediaPosts = null;
                }

                /*if ( post.Files?.Count > 0 )
                {
                    var medias = db.Medias.Where(m => post.Files.Contains(m.MediaID)).ToList();
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
                }*/

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
                    Code = (int)HttpStatusCode.InternalServerError,
                    HttpStatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
            }
        }
    }
}