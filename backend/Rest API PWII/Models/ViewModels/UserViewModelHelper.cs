using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UserViewModelHelper : UserViewModel
    {
        public UserMedia ProfilePic     { get; set; }
        public UserMedia CoverPic       { get; set; }
    }
}
