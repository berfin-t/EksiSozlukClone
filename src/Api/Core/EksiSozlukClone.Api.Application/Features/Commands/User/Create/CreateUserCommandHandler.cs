using AutoMapper;
using EksiSozlukClone.Api.Application.Interfaces.Repositories;
using EksiSozlukClone.Common.Infastructure.Exceptions;
using EksiSozlukClone.Common.Models.RequestModels;
using MediatR;
using EksiSozlukClone.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksiSozlukClone.Common.Infastructure;
using EksiSozlukClone.Common.Events.User;
using EksiSozlukClone.Common;

namespace EksiSozlukClone.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);
        if ( existUser is not null ) 
        {
            throw new DatabaseValidationException("User already exist");           
        }
        var dbUser = mapper.Map<Domain.Models.User>(request);

        var rows = await userRepository.AddAsync(dbUser);

        //Email Changed/Created
        if(rows >0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.EmailAddress
            };
  
    
            QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.UserExchangeName,
                exchangeType: SozlukConstants.DefaultExchangeType,
                queueName: SozlukConstants.UserEmailChangedQueueName,
                obj: @event);
        }

        return dbUser.Id;
    }
}
