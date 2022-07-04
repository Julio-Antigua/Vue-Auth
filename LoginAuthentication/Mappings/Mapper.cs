using AutoMapper;
using LoginAuthentication.Dtos;
using LoginAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginAuthentication.Mappings
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<User,RegisterDto>();
            CreateMap<RegisterDto,User>();

            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
        }
    }
}
