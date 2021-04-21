﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rest_API_PWII.Models.ViewModels
{
    public class UserViewModel
    {
        public string   Id                  { get; set; }

        public string   Nombre              { get; set; }

        public string   Tag                 { get; set; }

        public string   Email              { get; set; }

        public int?     FotoPerfilID        { get; set; }

        public DateTime? FechaNacimiento    { get; set; }
    }
}
