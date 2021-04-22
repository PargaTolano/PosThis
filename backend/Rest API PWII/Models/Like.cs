﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Like
    {

        public int LikeID { get; set; }

        public string UserID { get; set; }

        public int PostID { get; set; }

        public virtual User User { get; set; }

        public virtual Post Post { get; set; }

        public static readonly string ForeignKeyPost = "fk_Likes_Post";

        public static readonly string ForeignKeyUsuario = "fk_Likes_User";
    }
}
