using FatihAltuntasBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.Areas.Admin.Models
{
    public class CategoryAddAjaxViewModel
    {
        public CategoryAddDto CategoryAddDto { get; set; }
        public string CategoryAddPartial { get; set; }
        public CategoryDto CategoryDto { get; set; }
    }
}
