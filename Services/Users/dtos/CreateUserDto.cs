using System.Data;

namespace Blazor_FE.Services.Users.dtos;

public enum Role
{
    ADMIN,
    STAFF
}

public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string? FullName { get; set; }
    public string Role { get; set; }
}
