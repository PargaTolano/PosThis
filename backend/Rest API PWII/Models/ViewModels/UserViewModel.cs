using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UserViewModel
    {
        public string       Id                  { get; set; }
        public string       UserName            { get; set; }
        public string       Tag                 { get; set; }
        public string       Email               { get; set; }
        public string       ProfilePicPath      { get; set; }
        public string       CoverPicPath        { get; set; }
        public DateTime?    BirthDate           { get; set; }
        public int?         FollowerCount       { get; set; }
        public int?         FollowingCount      { get; set; }
    }
}
