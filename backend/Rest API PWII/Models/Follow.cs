using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Follow
    {
        public int FollowID { get; set; }

        public string UserFollowerID { get; set; }

        public string UserFollowID { get; set; }

        public virtual User UsuarioSeguidor { get; set; }

        public virtual User UsuarioSeguido { get; set;  }

        public static readonly string ForeignKeyUsuarioSeguidor = "fk_Follows_Usuarios_Seguidor";

        public static readonly string ForeignKeyUsuarioSeguido = "fk_Follows_Usuarios_Seguido";
    }
}
