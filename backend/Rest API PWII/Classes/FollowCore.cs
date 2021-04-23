using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Rest_API_PWII.Models;
using Rest_API_PWII.Models.ViewModels;

namespace Rest_API_PWII.Classes
{
    public class FollowCore
    {
        private PosThisDbContext db;

        public FollowCore( PosThisDbContext db )
        {
            this.db = db;
        }

        public ResponseApiError Validate( FollowViewModel model )
        {
            if ( model.FollowerID == null )
                return new ResponseApiError
                {
                    Code = ( int ) HttpStatusCode.BadRequest,
                    HttpStatusCode = ( int ) HttpStatusCode.BadRequest,
                    Message = "FollowerID does not accept null"
                };

            if ( model.FollowedID == null )
                return new ResponseApiError
                {
                    Code = ( int ) HttpStatusCode.BadRequest,
                    HttpStatusCode = ( int ) HttpStatusCode.BadRequest,
                    Message = "FollowedID does not accept null"
                };

            return null;
        }

        public ResponseApiError ValidateFollowExists( FollowViewModel model )
        {
            var follow = db.Follows.FirstOrDefault(f =>
                            f.UserFollowID == model.FollowedID &&
                            f.UserFollowerID == model.FollowerID);

            if ( follow != null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Follow already exists in database"
                };

            return null;
        }

        //TODO Get Followers and Followed

        public List<UserViewModel> GetFollowers( string FollowedID )
        {
            var usuario = db.Users.FirstOrDefault( u => u.Id == FollowedID );
            if ( usuario == null )
                throw new Exception("User not found, can not retrieve followers");

            var follows = 
                (from f in db.Follows
                 join u in db.Users
                 on f.UserFollowerID equals u.Id
                 where f.UserFollowID == FollowedID
                 select new UserViewModel { 
                    Id = f.UserFollowerID,
                    UserName = u.UserName,
                    Tag = u.Tag,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    ProfilePhotoMediaID = u.ProfilePhotoMediaID
                 }).DefaultIfEmpty().ToList();

            if (follows == null)
                return new List<UserViewModel>();

            return follows;
        }

        public List<UserViewModel> GetFollowing( string FollowerID )
        {
            var usuario = db.Users.FirstOrDefault(u => u.Id == FollowerID);
            if (usuario == null)
                throw new Exception("User not found, can not retrieve followed users");

            var follows =
                (from f in db.Follows
                 join u in db.Users
                 on f.UserFollowID equals u.Id
                 where f.UserFollowerID == FollowerID
                 select new UserViewModel
                 {
                     Id = f.UserFollowerID,
                     UserName = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     BirthDate = u.BirthDate,
                     ProfilePhotoMediaID = u.ProfilePhotoMediaID
                 }).DefaultIfEmpty().ToList();

            if (follows == null)
                return new List<UserViewModel>();

            return follows;
        }

        public int GetFollowersCount( string FollowedID )
        {
            var usuario = db.Users.FirstOrDefault(u => u.Id == FollowedID);
            if (usuario == null)
                throw new Exception("User not found, can not retrieve followers");

            var follows = GetFollowers( FollowedID );

            if (follows == null)
                return 0;

            return follows.Count;
        }

        public int GetFollowingCount( string FollowerID )
        {
            var usuario = db.Users.FirstOrDefault(u => u.Id == FollowerID);
            if (usuario == null)
                throw new Exception("User not found, can not retrieve followed users");

            var follows = GetFollowing( FollowerID );

            if (follows == null)
                return 0;

            return follows.Count;
        }

        public ResponseApiError Create( FollowViewModel model )
        {
            try
            {
                var err = Validate( model ) ;
                if ( err != null )
                    return err;

                err = ValidateFollowExists( model );
                if (err != null)
                    return err;

                var follower = db.Users.First( u => u.Id == model.FollowerID );
                var followed = db.Users.First( u => u.Id == model.FollowedID );

                var follow = new Follow
                {
                    UserFollowID    = followed.Id,
                    UserFollow      = followed,
                    UserFollowerID   = follower.Id,
                    UserFollower     = follower
                };

                db.Follows.Add( follow );
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

        public ResponseApiError Delete( int id )
        {
            try
            {
                var followDb = db.Follows.FirstOrDefault( u => u.FollowID == id );
                if ( followDb == null )
                    return new ResponseApiError
                    {
                        Code = (int)HttpStatusCode.NotFound,
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Message = "Follow does not exist"
                    };

                db.Follows.Remove( followDb );
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
