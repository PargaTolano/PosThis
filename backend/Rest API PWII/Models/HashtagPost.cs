using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class HashtagPost
    {

        public int HashtagPostID { get; set; }

        public int HashtagID { get; set; }
        
        public int PostID { get; set; }

        public virtual Hashtag Hashtag { get; set; }

        public virtual Post Post { get; set; }

        public static readonly string ForeignkeyHashtag = "fk_HashtagPost_Hashtag";

        public static readonly string ForeignkeyPost = "fk_HashtagPost_Post";
    }
}
