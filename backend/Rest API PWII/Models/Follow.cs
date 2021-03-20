using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Follow
    {
        public int FollowID { get; set; }

        public int UsuarioSeguidorID { get; set; }

        public int UsuarioSeguidoID { get; set; }

        public virtual Usuario UsuarioSeguidor { get; set; }

        public virtual Usuario UsuarioSeguido { get; set;  }

        public static readonly string ForeignKeyUsuarioSeguidor = "fk_Follows_Usuarios_Seguidor";

        public static readonly string ForeignKeyUsuarioSeguido = "fk_Follows_Usuarios_Seguido";
    }
}
