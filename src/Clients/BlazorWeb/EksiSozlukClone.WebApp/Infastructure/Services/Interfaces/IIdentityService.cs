using EksiSozlukClone.Common.Models.RequestModels;

namespace EksiSozlukClone.WebApp.Infastructure.Services.Interfaces;

public interface IIdentityService
{
    bool IsLoggedIn { get; }

    string GetUserToken();

    string GetUserName();

    Guid GetUserId();

    Task<bool> Login(LoginUserCommand command);

    void Logout();
}