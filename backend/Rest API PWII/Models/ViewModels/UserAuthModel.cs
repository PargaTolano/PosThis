using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UserAuthModel
    {
        public string Id                { get; set; }
        public string UserName          { get; set; }
        public string ProfilePicPath    { get; set; }
        public string Token             { get; set; }
    }
}