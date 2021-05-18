using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UReplyModel
    {
        public virtual string           Content { get; set; }
        public virtual List<int>        Deleted { get; set; }
        public virtual List<IFormFile>  Files { get; set; }
    }
}
