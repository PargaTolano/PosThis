using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class Usuario : IdentityUser
    {
        public virtual string UsuarioID { get; set; }

        public virtual string Nombre { get; set; }

        public virtual string Tag { get; set; }

        public virtual string Correo { get; set; }

        public virtual string Contrasena { get; set; }

        public virtual DateTime FechaNacimiento { get; set; }

        public virtual int FotoPerfilMediaID { get; set; }

        [JsonIgnore]
        public virtual Media FotoPerfil { get; set; }

        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Repost> Reposts { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like> Likes { get; set; }

        [JsonIgnore]
        public virtual ICollection<Reply> Replies { get; set; }

        [JsonIgnore]
        public virtual ICollection<Follow> Follows { get; set; }

        [JsonIgnore]
        public virtual ICollection<Follow> Following { get; set; }
    }
}
