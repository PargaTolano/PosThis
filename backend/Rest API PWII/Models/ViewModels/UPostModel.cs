using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UPostModel
    {
        public virtual string           Content { get; set; }
        public virtual List<int>        Deleted { get; set; }
        public virtual List<IFormFile>  Files   { get; set; }
    }
}
