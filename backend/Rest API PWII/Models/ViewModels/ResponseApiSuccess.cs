using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class ResponseApiSuccess
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
