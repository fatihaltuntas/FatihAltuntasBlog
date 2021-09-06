using AutoMapper;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Services.AutoMapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto, Category>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(o => DateTime.Now));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.CreatedDate, x => x.MapFrom(o => DateTime.Now));
            CreateMap<Category, CategoryUpdateDto>();
        }
    }
}
