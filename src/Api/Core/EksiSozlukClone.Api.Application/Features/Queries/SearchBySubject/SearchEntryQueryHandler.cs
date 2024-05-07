using EksiSozlukClone.Api.Application.Interfaces.Repositories;
using EksiSozlukClone.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Api.Application.Features.Queries.SearchBySubject;

public class SearchEntryQueryHandler: IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
{
    private readonly IEntryRepository entryRepository;

    public SearchEntryQueryHandler(IEntryRepository entryRepository)
    {
        this.entryRepository = entryRepository;
    }

    public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
    {
        var result = entryRepository
            //.Get(i => EF.Functions.Like(i.Subject, $"(request.SearchText)%"))
            .Get(i => i.Subject.ToLower().StartsWith(request.SearchText.ToLower()))
            .Select(i => new SearchEntryViewModel()
            {
                Id = i.Id,
                Subject = i.Subject,
            });

        return await result.ToListAsync(cancellationToken);
    }
}
