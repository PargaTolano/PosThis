using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class LikeViewModel
    {
        public virtual int? PostID { get; set; }

        public virtual string UserID { get; set; }
    }
}
