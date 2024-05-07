﻿using AutoMapper;
using EksiSozlukClone.Api.Application.Interfaces.Repositories;
using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Api.Application.Features.Queries.GetUserDetail;

public class GetUserDetailQueryHandler: IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        User dbUser = null;

        if (request.UserId != Guid.Empty)
            dbUser = await userRepository.GetByIdAsync(request.UserId);

        else if (!string.IsNullOrEmpty(request.UserName))
            dbUser = await userRepository.GetSingleAsync(i => i.UserName == request.UserName);

        return mapper.Map<UserDetailViewModel>(dbUser);
    }
}
