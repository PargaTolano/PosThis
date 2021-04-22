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

        public virtual User UserFollower { get; set; }

        public virtual User UserFollow { get; set;  }

        public static readonly string ForeignKeyUsuarioSeguidor = "fk_Follows_User_Follower";

        public static readonly string ForeignKeyUsuarioSeguido = "fk_Follows_User_Follows";
    }
}
