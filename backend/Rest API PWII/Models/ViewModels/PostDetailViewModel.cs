using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class PostDetailViewModel : PostViewModel
    {
        public virtual List<ReplyViewModel> Replies { get; set; }
    }
}
