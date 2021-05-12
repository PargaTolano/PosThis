using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class User : IdentityUser
    {
        public virtual string               Tag             { get; set; }
        public virtual DateTime?            BirthDate       { get; set; }

        public virtual int?                 ProfilePicID    { get; set; }
        public virtual int?                 CoverPicID      { get; set; }

        public virtual UserMedia            ProfilePic      { get; set; }
        public virtual UserMedia            CoverPic        { get; set; }
        public virtual List<Post>           Posts           { get; set; }
        public virtual List<Repost>         Reposts         { get; set; }
        public virtual List<Like>           Likes           { get; set; }
        public virtual List<Reply>          Replies         { get; set; }
        public virtual List<Follow>         Follows         { get; set; }
        public virtual List<Follow>         Following       { get; set; }
    }
}
