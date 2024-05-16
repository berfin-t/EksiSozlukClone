using EksiSozlukClone.Common.Events.User;
using EksiSozlukClone.Common.Infastructure.Exceptions;
using EksiSozlukClone.Common.Infastructure.Results;
using EksiSozlukClone.Common.Models.Queries;
using EksiSozlukClone.WebApp.Infastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace EksiSozlukClone.WebApp.Infastructure.Services;

public class UserService : IUserService
{
    private readonly HttpClient client;

    public UserService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
    {
        var userDetail = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");

        return userDetail;
    }

    public async Task<UserDetailViewModel> GetUserDetail(string userName)
    {
        var userDetail = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/username/{userName}");

        return userDetail;
    }

    public async Task<bool> UpdateUser(UserDetailViewModel user)
    {
        var result = await client.PostAsJsonAsync($"/api/user/update", user);

        return result.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
    {
        var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
        var httpResponse = await client.PostAsJsonAsync($"/api/User/ChangePassword", command);

        if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
        {
            if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseStr = await httpResponse.Content.ReadAsStringAsync();
                var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                responseStr = validation.FlattenErrors;
                throw new DatabaseValidationException(responseStr);
            }
            return false;
        }
        return httpResponse.IsSuccessStatusCode;
    }
}
