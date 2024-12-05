using CodeInk.Service.DTOs.User;
using System.Security.Claims;

namespace CodeInk.Service.Services.Interfaces;
public interface IUserService
{
    Task<UserDto?> LoginAsync(LoginDto input);
    Task<UserDto?> RegisterAsync(RegisterDto input);
    Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal User);
    Task<AddressDto> GetCurrentUserAddressAsync(ClaimsPrincipal User);
    Task<AddressDto> UpdateUserAddressAsync(ClaimsPrincipal User, AddressDto addressDto);
}
