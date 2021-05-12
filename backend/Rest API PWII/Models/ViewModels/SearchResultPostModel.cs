using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class SearchResultPostModel
    {
        public int                      PostID              { get; set; }
        public string                   Content             { get; set; }
        public string                   PublisherID         { get; set; }
        public string                   PublisherUserName   { get; set; }
        public string                   PublisherTag        { get; set; }
        public string                   PublisherProfilePic { get; set; }
        public DateTime?                PublishingTime      { get; set; }
        public List<MediaViewModel>     Medias              { get; set; }
        public int                      LikeCount           { get; set; }
        public int                      ReplyCount          { get; set; }
        public int                      RepostCount         { get; set; }
    }
}
