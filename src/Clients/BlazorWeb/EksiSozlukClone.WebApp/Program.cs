using Blazored.LocalStorage;
using EksiSozlukClone.WebApp;
using EksiSozlukClone.WebApp.Infastructure.Auth;
using EksiSozlukClone.WebApp.Infastructure.Services;
using EksiSozlukClone.WebApp.Infastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.IdentityModel.Tokens;

const string ClientName = "WepApiClient";

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddHttpClient(ClientName, client =>
{
    client.BaseAddress = new Uri("http://localhost:5001");
})
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient(ClientName);
});

builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddTransient<IVoteService, VoteService>();
builder.Services.AddTransient<IEntryService, EntryService>();
builder.Services.AddTransient<IFavService, FavService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
