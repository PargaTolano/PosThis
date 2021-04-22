using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UserViewModel
    {
        public string   Id                  { get; set; }

        public string   Name              { get; set; }

        public string   Tag                 { get; set; }

        public string   Email              { get; set; }

        public int?     ProfilePhotoMediaID        { get; set; }

        public DateTime? BirthDate    { get; set; }
    }
}
