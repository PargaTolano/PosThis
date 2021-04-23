using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class PostViewModel
    {
        public int          PostID              { get; set; }

        public string       Content             { get; set; }

        public List<int>    MediaIDs            { get; set; }

        public string       UserID              { get; set; }

        public string       UserName            { get; set; }

        public string       UserTag             { get; set; }

        public int?         UserProfilePicID    { get; set; }
    }
}