using EksiSozlukClone.Common.Models.Page;
using EksiSozlukClone.Common.Models.Queries;
using EksiSozlukClone.Common.Models.RequestModels;
using EksiSozlukClone.WebApp.Infastructure.Services.Interfaces;
using System.Net.Http.Json;

namespace EksiSozlukClone.WebApp.Infastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient client;

    public EntryService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<List<GetEntriesViewModel>> GetEntries()
    {
        var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEnties=false&count=30");
        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    {
        var result = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");
        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpagentries?page={page}&pagSize={pageSize}");
        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
    {
        var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");
        return result;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");
        return result;
    }

    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        var result = await client.PostAsJsonAsync("/api/Entry/CreateEntry", command);
        if (!result.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await result.Content.ReadAsStringAsync();
        return new Guid(guidStr.Trim('"'));

    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        var result = await client.PatchAsJsonAsync("/api/Entry/CreateEntryComment", command);
        if (result.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await result.Content.ReadAsStringAsync();
        return new Guid(guidStr.Trim('"'));
    }

    public async Task<List<SearchEntryViewModel>> SearchMySubject(string searchText)
    {
        var result = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");
        return result;
    }
}
