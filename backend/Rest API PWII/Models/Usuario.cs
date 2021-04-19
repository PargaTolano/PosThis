﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class Usuario : IdentityUser
    {
        public virtual string Tag { get; set; }

        public virtual DateTime? FechaNacimiento { get; set; }

        public virtual int? FotoPerfilMediaID { get; set; }

        public virtual Media FotoPerfil { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Repost> Reposts { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }

        public virtual ICollection<Follow> Following { get; set; }
    }
}
