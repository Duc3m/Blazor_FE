using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Blazor_FE.Services.Auth;

public class JwtInterceptor : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public JwtInterceptor(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Lấy token từ LocalStorage
            var token = await _localStorage.GetItemAsync<string>("authToken");

            // 2. Nếu có token, gắn vào Header Authorization
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        catch (InvalidOperationException)
        { 

        }
        catch (Exception)
        {
            // Bắt các lỗi khác nếu có để app không bị crash
        }
        // 3. Gửi request đi
        return await base.SendAsync(request, cancellationToken);
    }
}