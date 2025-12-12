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
    private readonly IServiceProvider _serviceProvider;
    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient, IServiceProvider serviceProvider)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
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
        await _localStorage.SetItemAsync("authToken", token);

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", ClaimTypes.Name, ClaimTypes.Role);
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task NotifyUserLogout()
    {
        await _localStorage.RemoveItemAsync("authToken");

        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        var claims = token.Claims.ToList();

        var roleClaims = claims.Where(c => c.Type == "role").ToList();
        if (roleClaims.Any())
        {
            foreach (var role in roleClaims)
            {
                claims.Remove(role);
                claims.Add(new Claim(ClaimTypes.Role, role.Value));
            }
        }

        var subClaim = claims.FirstOrDefault(c => c.Type == "sub");
        if (subClaim != null)
        {
            if (!claims.Any(c => c.Type == ClaimTypes.NameIdentifier))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, subClaim.Value));
            }
        }

        return claims;
    }

}