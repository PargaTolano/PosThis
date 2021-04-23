using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class Post
    {
        public int PostID { get; set; }

        public string Content { get; set; }

        public string UserID { get; set; }

        public DateTime PostDate { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<Repost> Reposts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like> Likes { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Reply> Replies { get; set; }

        [JsonIgnore]
        public virtual ICollection<HashtagPost> HashtagPosts { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<MediaPost> MediaPosts { get; set; }
        
        [JsonIgnore]
        public static readonly string ForeignKeyUsuario = "fk_Post_User";
    }
}
