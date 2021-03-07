using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Post
    {

        public int PostID { get; set; }

        public string Texto { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Repost> Reposts { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }


        public virtual ICollection<HashtagPost> HashtagPosts { get; set; }

        public virtual ICollection<MediaPost> MediaPosts { get; set; }

        public static readonly string ForeignKeyUsuario = "fk_Post_Usuario";
    }
}
