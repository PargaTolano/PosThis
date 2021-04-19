using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class LoginModel
    {
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
    }
}
