using EksiSozlukClone.Api.Application.Features.Commands.Entry.CreateFav;
using EksiSozlukClone.Api.Application.Features.Commands.Entry.DeleteFav;
using EksiSozlukClone.Api.Application.Features.Commands.EntryComment.CreateFav;
using EksiSozlukClone.Api.Application.Features.Commands.EntryComment.DeleteFav;
using EksiSozlukClone.Api.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EksiSozlukClone.Api.WebApi.Controllers;

public class FavoriteController : BaseController
{
    private readonly IMediator mediator;

    public FavoriteController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("entry/{entryId}")]
    public async Task<IActionResult> CreateEntryFav(Guid entryId)
    {
        var result = await mediator.Send(new CreateEntryFavCommand(entryId, UserId.Value));

        return Ok(result);
    }
    

    [HttpPost]
    [Route("entrycomment/{entrycommentId}")]
    public async Task<IActionResult> CreateEntryCommentFav(Guid entrycommentId)
    {
        var result = await mediator.Send(new CreateEntryCommentFavCommand(entrycommentId, UserId.Value));
        return Ok(result);
    }

    [HttpPost]
    [Route("deleteentryfav/{entryId}")]
    public async Task<IActionResult> DeleteEntryFav(Guid entryId)
    {
        var result = await mediator.Send(new DeleteEntryFavCommand(entryId, UserId.Value));
        return Ok(result);
    }

    [HttpPost]
    [Route("deleteentrycommentfav/{entrycommentId}")]
    public async Task<IActionResult> DeleteEntryCommentFav(Guid entrycommentId)
    {
        var result = await mediator.Send(new DeleteEntryCommentFavCommand(entrycommentId, UserId.Value));

        return Ok(result);
    }

}
