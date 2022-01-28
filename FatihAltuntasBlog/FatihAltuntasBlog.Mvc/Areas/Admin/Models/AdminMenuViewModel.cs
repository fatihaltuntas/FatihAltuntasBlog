﻿using FatihAltuntasBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.Areas.Admin.Models
{
    public class AdminMenuViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
