using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class UserCore
    {
        private PosThisDbContext db;

        public UserCore( PosThisDbContext db )
        {
            this.db = db;
        }

        public ResponseApiError Validate( User user )
        {
            if ( user.UserName == null || user.Tag == null || user.Email == null )
                return new ResponseApiError { 
                    Code = 1,
                    HttpStatusCode = (int) HttpStatusCode.BadRequest,
                    Message = "User data not valid" 
                };

            return null;
        }

        public ResponseApiError ValidateUpdate( UserViewModel model )
        {
            if( 
                model.UserName == null &&
                model.Tag == null &&
                model.Email == null &&
                model.ProfilePhotoMediaID == null &&
                model.BirthDate == null 
                )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Must edit at least one field" 
                };

            return null;
        }

        public ResponseApiError ValidateNewPassword( User user)
        {
            if ( user.PasswordHash == null )
                return new ResponseApiError
                {
                    Code = 1,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "User data not valid"
                };

            return null;
        }

        public ResponseApiError ValidateExists( User user )
        {
            var res = (from u in db.Users where u.Id == user.Id select u).First();

            if (res == null)
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound, 
                    Message = "User not found in database" 
                };

            return null;
        }

        public ResponseApiError ValidateExists( string id )
        {
            var res = (from u in db.Users where u.Id == id select u).First();

            if ( res == null )
                return new ResponseApiError { 
                    Code = 2,
                    HttpStatusCode = (int)HttpStatusCode.NotFound,
                    Message = "User not found in database"
                };

            return null;
        }

        public bool ValidateSearch (SearchRequestModel model)
        {
            return model.SearchPosts || model.SearchUsers;
        }
        
        public ResponseApiError Create( User user )
        {
            try
            {
                ResponseApiError err = Validate(user);
                if (err != null)
                    return err;

                db.Users.Add( user );
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
            List<UserViewModel> users = 
                (from u 
                 in db.Users 
                 select new UserViewModel {
                     Id = u.Id,
                     UserName = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     BirthDate = u.BirthDate,
                     ProfilePhotoMediaID = u.ProfilePhotoMediaID
                 }).ToList();
            return users;
        }

        public SearchResultModel GetSearch( SearchRequestModel model )
        {
            var valid = ValidateSearch( model );

            if ( !valid )
                return null;

            var searchResultModel = new SearchResultModel();

            if ( model.SearchPosts )
                searchResultModel.posts =
                    (from p in db.Posts
                     join u in db.Users
                     on p.UserID equals u.Id
                     where p.Content.ToLower().Contains( model.Query.ToLower().Trim() )
                     select new SearchResultPostModel
                     {
                         Content            = p.Content,
                         PublisherID        = u.Id,
                         PublisherUserName  = u.UserName,
                         PublisherTag       = u.Tag,
                         PublishingTime     = p.PostDate,
                     }).ToList();

            if ( model.SearchUsers )
                searchResultModel.users =
                    (from  u in db.Users
                     where u.NormalizedUserName.Contains( model.Query.ToUpper())
                     select new SearchResultUserModel
                     {
                         UserName       = u.UserName,
                         Tag            = u.Tag,
                         ProfilePicID   = u.ProfilePhotoMediaID,
                         FollowerCount  = (from   f in db.Follows
                                           where  f.UserFollowID == u.Id
                                           select f).Count()
                     }).ToList();

            return searchResultModel;
        }

        public UserViewModel GetOne( string id )
        {
            return (from u
                    in db.Users
                    where u.Id == id
                    select new UserViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Tag = u.Tag,
                        Email = u.Email,
                        BirthDate = u.BirthDate,
                        ProfilePhotoMediaID = u.ProfilePhotoMediaID
                    }).FirstOrDefault();
        }

        public ResponseApiError Update( string id, UserViewModel user )
        {
            try
            {
                var err = ValidateUpdate( user );
                if (err != null)
                    return err;

                err = ValidateExists( id );
                if (err != null)
                    return err;

                User userDb = db.Users.First( u => u.Id == id );

                userDb.Tag       = user.Tag != null ? user.Tag : userDb.Tag;

                userDb.UserName  = user.UserName != null ? user.UserName : userDb.UserName;
                userDb.NormalizedUserName = user.UserName != null ? user.UserName.ToUpper() : userDb.NormalizedUserName;

                userDb.Email     = user.Email != null ? user.Email : userDb.Email;
                userDb.NormalizedEmail = user.Email != null ? user.Email.ToUpper() : userDb.NormalizedEmail;

                userDb.BirthDate = user.BirthDate != null ? user.BirthDate : userDb.BirthDate;

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

                User userDb = db.Users.First( u => u.Id == id );

                db.Users.Remove( userDb );

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
