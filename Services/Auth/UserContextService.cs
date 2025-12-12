using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Blazor_FE.Services.Auth;

public class UserContextService
{
    private readonly AuthenticationStateProvider _authStateProvider;

    public UserContextService(AuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }

    public async Task<ClaimsPrincipal> GetCurrentUserAsync()
    {
        var state = await _authStateProvider.GetAuthenticationStateAsync();
        return state.User;
    }

    public async Task<bool> IsUserAuthenticatedAsync()
    {
        var user = await GetCurrentUserAsync();
        return user.Identity?.IsAuthenticated == true;
    }

    public async Task<int> GetUserIdAsync()
    {
        var user = await GetCurrentUserAsync();
        if (user.Identity?.IsAuthenticated == true)
        {
            var claim = user.FindFirst("userId") ?? user.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null && int.TryParse(claim.Value, out int id))
            {
                return id;
            }
        }
        return 0;
    }
}