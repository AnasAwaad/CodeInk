using CodeInk.Service.DTOs.User;

namespace CodeInk.Service.Services.Interfaces;
public interface IUserService
{
    Task<UserDto?> LoginAsync(LoginDto input);
    Task<UserDto?> RegisterAsync(RegisterDto input);
}
