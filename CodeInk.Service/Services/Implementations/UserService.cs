using CodeInk.Core.Entities.IdentityEntities;
using CodeInk.Service.DTOs.User;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CodeInk.Service.Services.Implementations;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    public async Task<UserDto?> LoginAsync(LoginDto input)
    {
        var user = _userManager.Users.Where(u => u.Email == input.Email || u.UserName == input.Email).FirstOrDefault();

        if (user is null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

        if (!result.Succeeded)
            throw new Exception($"Credential for {input.Email} are not valid");

        return new UserDto
        {
            Id = user.Id,
            Email = input.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.GenerateToken(user)
        };
    }

    public Task<UserDto> RegisterAsync(RegisterDto input)
    {
        throw new NotImplementedException();
    }
}
