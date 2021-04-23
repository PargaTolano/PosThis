using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SearchResultUserModel
    {
        public string UserName { get; set; }

        public string Tag { get; set; }

        public int FollowerCount { get; set; }

        public int MediaID { get; set; }
    }
}
