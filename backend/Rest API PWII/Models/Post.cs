using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class Post
    {
        public int PostID                               { get; set; }
        public string Content                           { get; set; }
        public DateTime PostDate                        { get; set; }

        public virtual User User                        { get; set; }
        public virtual List<Repost> Reposts             { get; set; }
        public virtual List<Like> Likes                 { get; set; }
        public virtual List<Reply> Replies              { get; set; }
        public virtual List<PostMedia> Medias               { get; set; }
        public virtual List<HashtagPost> HashtagPosts   { get; set; }
    }
}
