using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    //modelo para update reply
    public class ReplyViewModel
    {
        public int?                     ReplyID             { get; set; }
        public string                   Content             { get; set; }
        public int?                     PostID              { get; set; }
        public string                   PublisherID         { get; set; }
        public string                   PublisherUserName   { get; set; }
        public string                   PublisherTag        { get; set; }
        public string                   PublisherProfilePic { get; set; }
        public List<MediaViewModel>     Medias              { get; set; }
        public DateTime?                Date                { get; set; }
    }
}
