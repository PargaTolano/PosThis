using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SearchResultModel
    {
        public List<SearchResultPostModel>posts { get; set; }

        public List<SearchResultUserModel>users { get; set; }
    }
}
