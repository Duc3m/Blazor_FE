using Blazor_FE.Models;
using Blazor_FE.Services.Base;
using Blazor_FE.Services.Users.dtos;

namespace Blazor_FE.Services.Users;

public interface IUserService
{
    public Task<APIResponse<List<UserModel>>> GetUserPageAsync(int page, int pageSize, string searchUserName);

    public Task<CreateUserResult> CreateUserAsync(CreateUserDto createUserDto);
}
