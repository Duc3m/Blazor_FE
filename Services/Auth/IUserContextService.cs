using System.Security.Claims;

namespace Blazor_FE.Services.Auth;

public interface IUserContextService
{
    public Task<ClaimsPrincipal> GetCurrentUserAsync();
    public Task<bool> IsUserAuthenticatedAsync();
    public Task<int> GetUserIdAsync();
    public Task<Models.CustomerModel> GetCustomerByUserIdAsync();
}
