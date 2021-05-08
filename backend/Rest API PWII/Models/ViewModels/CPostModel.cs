using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class CPostModel
    {
        public virtual string           UserID      { get; set; }
        public virtual string           Content     { get; set; }
        public virtual List<IFormFile>  Files       { get; set; }
    }
}
