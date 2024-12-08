using AutoMapper;
using CodeInk.Core.Entities.IdentityEntities;
using CodeInk.Core.Exceptions;
using CodeInk.Service.DTOs.User;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CodeInk.Service.Services.Implementations;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.FindByEmailAsync(email!);

        return new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.DisplayName,
            // TODO: get user token that stored in db when make refresh token
        };

    }


    public async Task<AddressDto> GetCurrentUserAddressAsync(ClaimsPrincipal User)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);

        return _mapper.Map<AddressDto>(user!.Address);
    }


    public async Task<UserDto?> LoginAsync(LoginDto input)
    {
        var user = _userManager.Users.Where(u => u.Email == input.Email || u.UserName == input.Email).FirstOrDefault();

        if (user is null)
            throw new UnAuthorizedException("Username or Email Doesn't Exist");

        var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

        if (!result.Succeeded)
            throw new UnAuthorizedException();

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Email = input.Email,
            DisplayName = user.DisplayName,
            Token = _tokenService.GenerateToken(user, roles)
        };
    }

    public async Task<UserDto?> RegisterAsync(RegisterDto input)
    {
        var user = new ApplicationUser
        {
            Email = input.Email,
            UserName = input.UserName,
            DisplayName = input.DisplayName,
        };

        var result = await _userManager.CreateAsync(user, input.Password);
        var roleResult = await _userManager.AddToRoleAsync(user, "Customer");

        if (!result.Succeeded || !roleResult.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new ValidationException(errors);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            DisplayName = input.DisplayName,
            Email = input.Email,
            Token = _tokenService.GenerateToken(user, roles)
        };
    }

    public async Task<AddressDto> UpdateUserAddressAsync(ClaimsPrincipal User, AddressDto addressDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);

        user.Address = _mapper.Map(addressDto, user.Address);

        await _userManager.UpdateAsync(user);

        return addressDto;
    }
}
