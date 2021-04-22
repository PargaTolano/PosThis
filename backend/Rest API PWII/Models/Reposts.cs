using System;

namespace Rest_API_PWII.Models
{
    public class Reposts
    {

        public int RepostID { get; set; }

        public string Content { get; set; }

        public DateTime? RepostDate { get; set; }

        public string UserID { get; set; }

        public int PostID { get; set; }

        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public static readonly string ForeignKeyPost = "fk_Repost_Post";

        public static readonly string ForeignKeyUsuario = "fk_Repost_User";
    }
}
