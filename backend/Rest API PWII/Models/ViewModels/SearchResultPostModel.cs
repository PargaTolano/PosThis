using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SearchResultPostModel
    {
        public string       Content             { get; set; }

        public string       PublisherID         { get; set; }

        public string       PublisherUserName   { get; set; }

        public string       PublisherTag        { get; set; }

        public DateTime?    PublishingTime      { get; set; }

        public List<int>    MediaIDs            { get; set; }
    }
}
