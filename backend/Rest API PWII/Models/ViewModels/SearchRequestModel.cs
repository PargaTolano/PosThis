using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SearchRequestModel
    {
        public bool         SearchPosts { get; set; }

        public bool         SearchUsers { get; set; }

        public string       Query       { get; set; }

        public List<string> Hashtags    { get; set; }
    }
}
