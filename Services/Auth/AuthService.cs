using Blazor_FE.Models;
using Blazor_FE.Models.Auth;
using Blazor_FE.Services.Base;
using System.Text.Json;

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

    public async Task<APIResponse<UserData>> RegisterAsync(RegisterRequest model)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/user-register", model);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                try
                {
                    // Thử phân tích lỗi dưới dạng cấu trúc APIResponse chuẩn
                    var errorResult = JsonSerializer.Deserialize<APIResponse<UserData>>(errorContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (errorResult != null && !string.IsNullOrWhiteSpace(errorResult.Message))
                    {
                        return errorResult;
                    }
                }
                catch { } // Bỏ qua nếu không phân tích được

                // Nếu không phân tích được, trả về nội dung lỗi thô
                return new APIResponse<UserData>
                {
                    StatusCode = (int)response.StatusCode,
                    Message = errorContent, // Sử dụng nội dung lỗi từ API
                    Content = null
                };
            }

            var result = await response.Content.ReadFromJsonAsync<APIResponse<UserData>>();
            return result;
        } catch (Exception ex) 
        {
            return new APIResponse<UserData>
            {
                StatusCode = 404,
                Message = ex.Message
            };
        }
    }
}
