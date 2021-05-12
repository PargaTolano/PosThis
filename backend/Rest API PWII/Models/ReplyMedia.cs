using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class ReplyMedia : Media
    {
        public virtual Reply Reply { get; set; }
    }
}
