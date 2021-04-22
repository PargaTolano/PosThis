using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Hashtag
    {
        public int HashtagID { get; set; }

        public string ContentHashtag { get; set; }

        public virtual ICollection<HashtagPost> HashtagPosts { get; set; }
    }
}
