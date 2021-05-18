using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    //modelo para update reply
    public class ReplyViewModel
    {
        public virtual int?                     ReplyID             { get; set; }
        public virtual string                   Content             { get; set; }
        public virtual int?                     PostID              { get; set; }
        public virtual string                   PublisherID         { get; set; }
        public virtual string                   PublisherUserName   { get; set; }
        public virtual string                   PublisherProfilePic { get; set; }
        public virtual List<MediaViewModel>     Medias              { get; set; }
        public virtual DateTime?                Date                { get; set; }
    }
}
