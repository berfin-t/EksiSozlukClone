using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace EksiSozlukClone.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile() 
    { 
        CreateMap<User, LoginUserViewModel>().ReverseMap();
    }
}
