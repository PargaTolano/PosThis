﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Reply
    {
        public int ReplyID { get; set; }

        public int PostID { get; set; }

        public string UserID { get; set; }

        public string ContentReplies { get; set; }

        public virtual Post Post { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<MediaReply> MediaReplies { get; set; }

        public static readonly string ForeignKeyPost = "fk_Replies_Post";

        public static readonly string ForeignKeyUsuario = "fk_Replies_User";
    }
}
