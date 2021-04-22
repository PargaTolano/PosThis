using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    //modelo para update reply
    public class CUReplyModel
    {
        public virtual int?         ReplyID          { get; set; }

        public virtual string       Content         { get; set; }

        public virtual int?         PostID          { get; set; }

        public virtual string       UserID     { get; set; }

        public virtual List<int>    mediaIDs   { get; set; }
    }
}
