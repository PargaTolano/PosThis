using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    //modelo para update reply
    public class UpdateReplyModel
    {
        //id del reply a actualizar
        public virtual int ReplyID          { get; set; }

        //texto del reply
        public virtual string Texto         { get; set; }

        //ids media que va a utilizar
        public virtual List<int> mediaIDs   { get; set; }
    }
}
