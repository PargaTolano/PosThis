using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Rest_API_PWII.Models.ViewModels
{
    public class CPostModel
    {
        public virtual string           UserID      { get; set; }
        public virtual string           Content     { get; set; }
        public virtual List<IFormFile>  Files       { get; set; }
    }
}
