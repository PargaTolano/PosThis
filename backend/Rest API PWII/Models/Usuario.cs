using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Repost> Reposts { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<Follow> Follows { get; set; }
    }
}
