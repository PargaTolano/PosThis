using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class CreatePostModel
    {
        public virtual string usuarioID     { get; set; }
        public virtual string Texto         { get; set; }
        public virtual List<int> mediaIDs   { get; set; }
    }
}
