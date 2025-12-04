using Blazor_FE.Models.Auth;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<APIResponse<Dictionary<String, String>>> LoginAsync(LoginRequest model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/login", model);
            if(!response.IsSuccessStatusCode)
            {
                return new APIResponse<Dictionary<string, string>>
                {
                    StatusCode = (int)response.StatusCode,
                    Message = "Login failed",
                    Content = null
                };
            }
            var result = await response.Content.ReadFromJsonAsync<APIResponse<Dictionary<String, String>>>();
            return result;
        }
        catch (Exception ex)
        {
            return new APIResponse<Dictionary<string, string>>
            {
                StatusCode = 404,
                Message = ex.Message
            };
        }
    }
}
