using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Api.Application.Features.Commands.Entry.DeleteFav;

public class DeleteEntryFavComand: IRequest<bool>
{
    public Guid EntryId { get; set; }
    public Guid UserId { get; set; }

    public DeleteEntryFavComand(Guid entryId, Guid userId)
    {
        EntryId = entryId;
        UserId = userId;
    }
}
