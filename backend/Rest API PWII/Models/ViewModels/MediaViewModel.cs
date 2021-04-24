using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class MediaViewModel
    {
        public int MediaID { get; set; }

        public string MIME { get; set; }

        public byte[] Content { get; set; }
    }
}
