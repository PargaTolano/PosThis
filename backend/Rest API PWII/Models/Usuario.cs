using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rest_API_PWII.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public string Nombre { get; set; }

        public string Tag { get; set; }

        public string Correo { get; set; }

        public string Contrasena { get; set; }

        public DateTime FechaNacimiento { get; set; }

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
    }
}
