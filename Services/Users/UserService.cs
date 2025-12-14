using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Users.dtos;
using Microsoft.AspNetCore.WebUtilities;

namespace Blazor_FE.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<APIResponse<List<UserModel>>> GetUserPageAsync(int page, int pageSize,string searchUserName)
    {
        try
        {
            var queryParams = new Dictionary<string, string?>
            {
                ["pageNumber"] = page.ToString(),
                ["pageSize"] = pageSize.ToString()
            };

            var url = QueryHelpers.AddQueryString($"api/v1/User?Username={searchUserName}", queryParams);

            var response = _httpClient.GetFromJsonAsync<APIResponse<List<UserModel>>>(url);
            return await response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CreateUserResult> CreateUserAsync(CreateUserDto createUserDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/v1/User", createUserDto);
            if (response.IsSuccessStatusCode)
            {
                var responseResult = await response.Content.ReadFromJsonAsync<APIResponse<UserModel>>();
                var result = new CreateUserResult
                {
                    isSuccess = true,
                    message = "User created successfully"
                };
                return result!;
            }

            var responseContent = await response.Content.ReadFromJsonAsync<Dictionary<string,object>>();

            return new CreateUserResult
            {
                isSuccess = false,
                message = responseContent["message"]?.ToString() ?? "Failed to create user"
            };
        }
        catch (Exception ex)
        {
            return new CreateUserResult
            {
                isSuccess = false,
                message = ex.Message
            };
        }
    }

}
