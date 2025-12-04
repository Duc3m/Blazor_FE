using Blazor_FE.Models.Auth;
using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Auth;

public interface IAuthService
{
    public Task<APIResponse<Dictionary<String, String>>> LoginAsync(LoginRequest model);
}
