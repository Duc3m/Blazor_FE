using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Blazor_FE.Services.Cart;

namespace Blazor_FE.Services.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    private readonly ICartService _cartService;
    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient, ICartService cartService)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
        _cartService = cartService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = null;
        try
        {
            token = await _localStorage.GetItemAsync<string>("authToken");
        }
        catch
        {

        }

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", ClaimTypes.Name, ClaimTypes.Role);
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public async Task NotifyUserAuthentication(string token)
    {
        // 1. Lưu token vào LocalStorage
        await _localStorage.SetItemAsync("authToken", token);

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", ClaimTypes.Name, ClaimTypes.Role);
        var user = new ClaimsPrincipal(identity);

        // 2. Tải giỏ hàng của người dùng sau khi đăng nhập
        await _cartService.LoadCartFromStorageAsync();

        // 3. Thông báo cho toàn bộ ứng dụng về trạng thái đăng nhập mới
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    // Thêm phương thức để xử lý đăng xuất
    public async Task NotifyUserLogout()
    {
        // 1. Xóa token khỏi LocalStorage
        await _localStorage.RemoveItemAsync("authToken");

        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);

        // 2. Tải lại giỏ hàng (lúc này sẽ là giỏ hàng của khách)
        await _cartService.LoadCartFromStorageAsync();

        // 3. Thông báo cho toàn bộ ứng dụng về trạng thái đăng xuất
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        var claims = token.Claims.ToList();
        var subClaim = claims.FirstOrDefault(c => c.Type == "sub");
        if (subClaim != null)
        {
            // Add the NameIdentifier claim if it doesn't exist, using the value from 'sub'
            if (!claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, subClaim.Value));
            }
        }
        return claims;
    }
}