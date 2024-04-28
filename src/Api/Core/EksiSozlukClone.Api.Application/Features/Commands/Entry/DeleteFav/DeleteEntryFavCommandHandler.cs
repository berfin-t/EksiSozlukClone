using EksiSozlukClone.Common;
using EksiSozlukClone.Common.Events.Entry;
using EksiSozlukClone.Common.Infastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Api.Application.Features.Commands.Entry.DeleteFav;

public class DeleteEntryFavCommandHandler: IRequestHandler<DeleteEntryFavComand, bool>
{
    public async Task<bool> Handle(DeleteEntryFavComand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SozlukConstants.FavExchangeName,
            exchangeType: SozlukConstants.DefaultExchangeType,
            queueName: SozlukConstants.DeleteEntryFavQueueName,
            obj: new DeleteEntryFavEvent()
            {
                EntryId = request.EntryId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }

}
