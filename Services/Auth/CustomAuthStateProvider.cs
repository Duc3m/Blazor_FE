using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Blazor_FE.Services.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;
    public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
    {
        _localStorage = localStorage;
        _httpClient = httpClient;
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

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", "name", "role");
        var user = new ClaimsPrincipal(identity);

        //_httpClient.DefaultRequestHeaders.Authorization =
        //    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt", "name", "role");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}