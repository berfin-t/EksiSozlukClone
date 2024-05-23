using Blazored.LocalStorage;
using EksiSozlukClone.Common.Infastructure.Results;
using EksiSozlukClone.Common.Models.Queries;
using EksiSozlukClone.Common.Models.RequestModels;
using EksiSozlukClone.WebApp.Infastructure.Auth;
using EksiSozlukClone.WebApp.Infastructure.Extensions;
using EksiSozlukClone.WebApp.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace EksiSozlukClone.WebApp.Infastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly HttpClient client;
    private readonly ISyncLocalStorageService syncLocalStorageService;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorageService)
    {
        this.client = client;
        this.syncLocalStorageService = syncLocalStorageService;
    }

    public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

    public string GetUserToken()
    {
        return syncLocalStorageService.GetToken();
    }

    public string GetUserName()
    {
        return syncLocalStorageService.GetToken();
    }

    public Guid GetUserId()
    {
        return syncLocalStorageService.GetUserId();
    }

    public async Task<bool> Login(LoginUserCommand command)
    {
        string responseStr;
        var httpResponse = await client.PostAsJsonAsync("/api/User/Login", command);

        if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
        {
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                responseStr = await httpResponse.Content.ReadAsStringAsync();
                var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                responseStr = validation.FlattenErrors;
                throw new DataMisalignedException(responseStr);
            }

            return false;
        }

        responseStr = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

        if (!string.IsNullOrEmpty(response.Token)) //login success
        {
            syncLocalStorageService.SetToken(response.Token);
            syncLocalStorageService.SetUserName(response.UserName);
            syncLocalStorageService.SetUserId(response.Id);

            ((AuthStateProvider)authenticationStateProvider).NotifyUserLogin(response.UserName, response.Id);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);

            return true;
        }

        return false;
    }

    public void Logout()
    {
        syncLocalStorageService.RemoveItem(LocalStorageExtensions.TokenName);
        syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserName);
        syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserId);

        ((AuthStateProvider)authenticationStateProvider).NotifyUserLogout();
        client.DefaultRequestHeaders.Authorization = null;
    }
}
