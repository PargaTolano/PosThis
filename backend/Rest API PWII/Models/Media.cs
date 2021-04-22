using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Media
    {
        public int MediaID { get; set; }

        public string MIME { get; set; }

        public byte[] Content { get; set; }

        public virtual ICollection<MediaPost> MediaPosts { get; set; }

        public virtual ICollection<MediaReply> MediaReplies { get; set; }
    }
}
