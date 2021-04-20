using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class Post
    {
        public int PostID { get; set; }

        public string Texto { get; set; }

        public string UsuarioID { get; set; }

        public DateTime FechaPublicacion { get; set; }

        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reposts> Reposts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Likes> Likes { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Reply> Replies { get; set; }

        [JsonIgnore]
        public virtual ICollection<HashtagPost> HashtagPosts { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<MediaPost> MediaPosts { get; set; }
        
        [JsonIgnore]
        public static readonly string ForeignKeyUsuario = "fk_Post_Usuario";
    }
}
