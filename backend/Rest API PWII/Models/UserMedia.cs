using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class UserMedia : Media
    {
        public virtual User ProfilePicOwner { get; set; }
        public virtual User CoverPicOwner { get; set; }
    }
}
