using Bogus.Bson;
using EksiSozlukClone.Api.Application.Features.Commands.Entry.DeleteVote;
using EksiSozlukClone.Api.Application.Features.Commands.EntryComment.DeleteVote;
using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Common.Models.RequestModels;
using EksiSozlukClone.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EksiSozlukClone.Api.WebApi.Controllers;

public class VoteController : Controller
{
    private readonly IMediator mediator;

    public VoteController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("Entry/{entryId}")]
    public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var result = await mediator.Send(new CreateEntryVoteCommand(entryId, voteType, UserId));

        return Ok(result);
    }

    [HttpPost]
    [Route("EntryComment/{entryCommentId}")]
    public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("DeleteEntryVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryVote(Guid entryId)
    {
        var result = await mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

        return Ok();
    }

    [HttpPost]
    [Route("DeleteEntryCommentVote/{entryId}")]
    public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
    {
        var result = await mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

        return Ok();
    }
}
