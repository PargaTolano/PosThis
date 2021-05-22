using System;
using System.Collections.Generic;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SearchResultPostModel : PostViewModel
    {
        public bool IsLiked         { get; set; }
        public bool IsReposted      { get; set; }
    }
}
