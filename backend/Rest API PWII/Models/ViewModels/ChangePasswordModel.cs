using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class ChangePasswordModel
    {
        public string CurrentPassword   {get; set;}
        public string NewPassword       { get; set; }
    }
}
