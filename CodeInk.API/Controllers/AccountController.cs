using CodeInk.Application.DTOs;
using CodeInk.Service.DTOs.User;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class AccountController : APIBaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto input)
    {
        var result = await _userService.LoginAsync(input);
        if (result is null)
            return BadRequest(new ApiResponse(400, "Email Does not exist"));

        return Ok(new ApiResponse(200, "Login successful", result));
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto input)
    {
        var result = await _userService.RegisterAsync(input);
        if (result is null)
            return BadRequest(new ApiResponse(400, "Username or Email Already Exists"));

        return Ok(new ApiResponse(200, "Registration successful", result));
    }
}
