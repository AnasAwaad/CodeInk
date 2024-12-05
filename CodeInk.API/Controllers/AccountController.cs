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

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto input)
    {
        var result = await _userService.LoginAsync(input);
        if (result is null)
            return BadRequest(new ApiResponse(400, "Email Does not exist"));

        return Ok(new ApiResponse(200, data: result));
    }
}
