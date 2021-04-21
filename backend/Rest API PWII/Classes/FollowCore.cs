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
                    Message = "FollowerID no puede ser null"
                };

            if ( model.FollowedID == null )
                return new ResponseApiError
                {
                    Code = ( int ) HttpStatusCode.BadRequest,
                    HttpStatusCode = ( int ) HttpStatusCode.BadRequest,
                    Message = "FollowedID no puede ser null"
                };

            return null;
        }

        public ResponseApiError ValidateFollowExists( FollowViewModel model )
        {
            var follower = db.Usuarios.FirstOrDefault( u => u.Id == model.FollowerID );
            if ( follower == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Follower no puede ser null"
                };

            var followed = db.Usuarios.FirstOrDefault( u => u.Id == model.FollowedID );
            if ( followed == null )
                return new ResponseApiError
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    HttpStatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Followed no puede ser null"
                };

            return null;
        }

        //TODO Get Followers and Followed

        public List<UserViewModel> GetFollowers( string FollowedID )
        {
            var usuario = db.Usuarios.FirstOrDefault( u => u.Id == FollowedID );
            if ( usuario == null )
                throw new Exception("No existe el usuario del cual se buscan followers");

            var follows = 
                (from f in db.Follows
                 join u in db.Usuarios
                 on f.UsuarioSeguidoID equals u.Id
                 select new UserViewModel { 
                    Id = f.UsuarioSeguidorID,
                    Nombre = u.UserName,
                    Tag = u.Tag,
                    Email = u.Email,
                    FechaNacimiento = u.FechaNacimiento,
                    FotoPerfilID = u.FotoPerfilMediaID
                 }).DefaultIfEmpty().ToList();

            if (follows == null)
                return new List<UserViewModel>();

            return follows;
        }

        public List<UserViewModel> GetFollowing( string FollowerID )
        {
            var usuario = db.Usuarios.FirstOrDefault(u => u.Id == FollowerID);
            if (usuario == null)
                throw new Exception("No existe el usuario del cual se buscan followings");

            var follows =
                (from f in db.Follows
                 join u in db.Usuarios
                 on f.UsuarioSeguidorID equals u.Id
                 select new UserViewModel
                 {
                     Id = f.UsuarioSeguidorID,
                     Nombre = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     FechaNacimiento = u.FechaNacimiento,
                     FotoPerfilID = u.FotoPerfilMediaID
                 }).DefaultIfEmpty().ToList();

            if (follows == null)
                return new List<UserViewModel>();

            return follows;
        }

        public int GetFollowersCount( string FollowedID )
        {
            var usuario = db.Usuarios.FirstOrDefault(u => u.Id == FollowedID);
            if (usuario == null)
                throw new Exception("No existe el usuario del cual se buscan followers");

            var follows =
                (from f in db.Follows
                 join u in db.Usuarios
                 on f.UsuarioSeguidoID equals u.Id
                 select new UserViewModel
                 {
                     Id = f.UsuarioSeguidorID,
                     Nombre = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     FechaNacimiento = u.FechaNacimiento,
                     FotoPerfilID = u.FotoPerfilMediaID
                 }).DefaultIfEmpty().ToList();

            if (follows == null)
                return 0;

            return follows.Count;
        }

        public int GetFollowingCount( string FollowedID )
        {
            var usuario = db.Usuarios.FirstOrDefault(u => u.Id == FollowedID);
            if (usuario == null)
                throw new Exception("No existe el usuario del cual se buscan followings");

            var follows =
                (from f in db.Follows
                 join u in db.Usuarios
                 on f.UsuarioSeguidorID equals u.Id
                 select new UserViewModel
                 {
                     Id = f.UsuarioSeguidorID,
                     Nombre = u.UserName,
                     Tag = u.Tag,
                     Email = u.Email,
                     FechaNacimiento = u.FechaNacimiento,
                     FotoPerfilID = u.FotoPerfilMediaID
                 }).DefaultIfEmpty().ToList();

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

                var follower = db.Usuarios.First( u => u.Id == model.FollowerID );
                var followed = db.Usuarios.First( u => u.Id == model.FollowedID );

                var follow = new Follow
                {
                    UsuarioSeguidoID    = followed.Id,
                    UsuarioSeguido      = followed,
                    UsuarioSeguidorID   = follower.Id,
                    UsuarioSeguidor     = follower
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
                        Message = "Hashtag no existe"
                    };

                db.Follows.Remove( followDb );

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
