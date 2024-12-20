﻿using CodeInk.API.Errors;
using CodeInk.Service.DTOs.Order;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeInk.API.Controllers;

[Authorize(Roles = "Customer")]
public class OrdersController : APIBaseController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderRequestDto request)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var order = await _orderService.CreateOrderAsync(request, userEmail!);

        return Ok(new ApiResponse(201, "Order Created Successfully", order));
    }


    [HttpGet]
    public async Task<IActionResult> GetOrdersByEmail(string email)
    {
        var orders = await _orderService.GetOrdersByEmailAsync(email);
        return Ok(new ApiResponse(200, "Orders retrived successfully", orders));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return Ok(new ApiResponse(200, "Order retrived successfully", order));
    }

    [HttpGet("DeliveryMethods")]
    public async Task<IActionResult> GetDeliveryMethods()
    {
        var methods = await _orderService.GetAllDeliveryMethodsAsync();
        return Ok(new ApiResponse(200, "Delivery methods retrived successfully", methods));
    }
}
