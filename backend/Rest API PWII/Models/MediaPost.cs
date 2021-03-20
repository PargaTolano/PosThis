using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class MediaPost
    {
        public virtual int MediaID { get; set; }

        public virtual int PostID { get; set; }

        public virtual Media Media { get; set; }

        public virtual Post Post { get; set; }

        public static readonly string ForeignkeyMedia = "fk_MediaPost_Media";

        public static readonly string ForeignkeyPost = "fk_MediaPost_Post";
    }
}
