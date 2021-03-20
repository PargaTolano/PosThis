using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class ResponseApiError
    {
        public int Code { get; set; }

        public int HttpStatusCode { get; set; }

        public string Message { get; set; }
    }
}
