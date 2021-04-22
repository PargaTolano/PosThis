using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class User : IdentityUser
    {
        public virtual string Tag { get; set; }

        public virtual DateTime? BirthDate { get; set; }

        public virtual int? ProfilePhotoMediaID { get; set; }

        public virtual Media ProfilePhotoMedia { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Reposts> Reposts { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }

        public virtual ICollection<Follow> Following { get; set; }
    }
}
