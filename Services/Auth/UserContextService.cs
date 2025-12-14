using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Blazor_FE.Models;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Auth;

public class UserContextService : IUserContextService
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly HttpClient _httpClient;

    public UserContextService(AuthenticationStateProvider authStateProvider, HttpClient httpClient)
    {
        _authStateProvider = authStateProvider;
        _httpClient = httpClient;
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

    public async Task<CustomerModel> GetCustomerByUserIdAsync() 
    {
        int userId = await GetUserIdAsync();
        var url = $"api/v1/Customer/user/{userId}";
        var response = await _httpClient.GetFromJsonAsync<APIResponse<CustomerModel>>(url);
        return response.Content;
    }
}