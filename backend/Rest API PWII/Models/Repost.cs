using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Repost
    {

        public int RepostID { get; set; }

        public string Texto { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string UsuarioID { get; set; }

        public int PostID { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual Post Post { get; set; }

        public static readonly string ForeignKeyPost = "fk_Repost_Post";

        public static readonly string ForeignKeyUsuario = "fk_Repost_Usuario";
    }
}
