using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class CRepostModel
    {
        public string UserID { get; set; }

        public int? PostID { get; set; }

        public DateTime? RepostDate { get; set; }
    }
}
