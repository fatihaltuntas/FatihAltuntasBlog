using AutoMapper;
using FatihAltuntasBlog.Entities.Concrete;
using FatihAltuntasBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Mvc.AutoMapper.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}
