using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Hashtag
    {
        public int HastagID { get; set; }

        public string Texto { get; set; }

        public virtual ICollection<HashtagPost> HashtagPosts { get; set; }
    }
}
