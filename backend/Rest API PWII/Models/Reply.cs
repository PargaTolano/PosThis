using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Reply
    {
        public int ReplyID { get; set; }

        public int PostID { get; set; }

        public int UsuarioID { get; set; }

        public virtual Post Post { get; set; }

        public virtual Usuario Usuario { get; set; }

        public static readonly string ForeignKeyPost = "fk_Replies_Post";

        public static readonly string ForeignKeyUsuario = "fk_Replies_Usuario";
    }
}
