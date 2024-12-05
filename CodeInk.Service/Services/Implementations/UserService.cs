using CodeInk.Core.Entities.IdentityEntities;
using CodeInk.Service.DTOs.User;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<UserDto?> RegisterAsync(RegisterDto input)
    {
        var isExist = await _userManager.Users.Where(u => u.Email == input.Email || u.UserName == input.UserName).AnyAsync();

        if (isExist)
            return null;

        var user = new ApplicationUser
        {
            Email = input.Email,
            UserName = input.UserName,
            DisplayName = input.DisplayName,
        };

        var result = await _userManager.CreateAsync(user, input.Password);

        if (!result.Succeeded)
            throw new Exception($"Error creating user: {string.Join(',', result.Errors.SelectMany(e => e.Description))}");

        return new UserDto
        {
            Id = user.Id,
            DisplayName = input.DisplayName,
            Email = input.Email,
            Token = _tokenService.GenerateToken(user),
        };
    }
}
