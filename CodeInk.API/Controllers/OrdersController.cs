using CodeInk.API.Errors;
using CodeInk.Service.DTOs.Order;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeInk.API.Controllers;

public class OrdersController : APIBaseController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> CreateOrder(OrderRequestDto request)
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var order = await _orderService.CreateOrderAsync(request, userEmail!);

        return Ok(new ApiResponse(201, "Order Created Successfully", order));
    }
}
