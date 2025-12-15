using System.Security.Claims;

namespace Blazor_FE.Services.Auth;

public interface IUserContextService
{
    public Task<ClaimsPrincipal> GetCurrentUserAsync();
    public Task<bool> IsUserAuthenticatedAsync();
    public Task<int> GetCurrentUserIdAsync();
    public Task<Models.CustomerModel> GetCurrentCustomerAsync();
}
