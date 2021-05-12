using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models
{
    public class Reply
    {
        public int                      ReplyID         { get; set; }
        public string                   ContentReplies  { get; set; }
        public string                   UserID          { get; set; }
        public int                      PostID          { get; set; }
        public virtual Post             Post            { get; set; }
        public virtual User             User            { get; set; }
        public virtual List<ReplyMedia> Medias          { get; set; }
    }
}
