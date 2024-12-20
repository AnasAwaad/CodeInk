﻿using CodeInk.API.Errors;
using CodeInk.Service.DTOs.User;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        return Ok(new ApiResponse(200,
                                        "Login successful",
                                        await _userService.LoginAsync(input)));
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto input)
    {
        return Ok(new ApiResponse(200,
                                        "Registration successful",
                                        await _userService.RegisterAsync(input)));
    }

    [Authorize]
    [HttpGet("GetCurrentUser")]
    public async Task<IActionResult> GetCurrentUser()
    {
        return Ok(new ApiResponse(200,
                                        "User retrived successfully",
                                        await _userService.GetCurrentUserAsync(User)));
    }


    [Authorize]
    [HttpGet("Address")]
    public async Task<IActionResult> GetCurrentUserAddress()
    {
        return Ok(new ApiResponse(200,
                                        "User address retrived successfully",
                                        await _userService.GetCurrentUserAddressAsync(User)));
    }

    [Authorize]
    [HttpPut("Address")]
    public async Task<IActionResult> UpdateUserAddress(AddressDto address)
    {
        return Ok(new ApiResponse(200,
                                        "Address updated successfully",
                                        await _userService.UpdateUserAddressAsync(User, address)));
    }
}
