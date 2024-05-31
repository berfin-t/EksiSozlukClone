using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EksiSozlukClone.Common.Models.RequestModels;

namespace EksiSozlukClone.Api.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile() 
    { 
        CreateMap<User, LoginUserViewModel>().ReverseMap();
        CreateMap<CreateUserCommand,User>().ReverseMap();
        CreateMap<UpdateUserCommand, User>().ReverseMap();
        CreateMap<CreateEntryCommand, Entry>().ReverseMap();
        CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap(); 
        CreateMap<Entry, GetEntriesViewModel>()
            .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));
        CreateMap<UserDetailViewModel, User>().ReverseMap();
    }
}
