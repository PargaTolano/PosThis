using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SignUpModel
    {
        public string UserName      { get; set; }
        public string Tag           { get; set; }
        public string Email         { get; set; }
        public string Password      { get; set; }
    }
}
