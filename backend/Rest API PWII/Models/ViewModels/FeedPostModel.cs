using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class FeedPostModel
    {
        public bool                     IsRepost    { get; set; }

        public string                   Content     { get; set; }

        public string                   PublisherID { get; set; }

        public int?                     PostID      { get; set; }

        public string                   ReposterID  { get; set; }

        public List<MediaViewModel>     Medias      { get; set; }

        public DateTime?                Date        { get; set; }
    }
}
