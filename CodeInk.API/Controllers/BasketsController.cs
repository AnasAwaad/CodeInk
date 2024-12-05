using CodeInk.Application.DTOs;
using CodeInk.Service.DTOs.Basket;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class BasketsController : APIBaseController
{
    private readonly IBasketService _basketService;

    public BasketsController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet]
    public async Task<ActionResult> GetBasketById(string basketId)
    {
        var basket = await _basketService.GetBasketAsync(basketId);
        return Ok(new ApiResponse(200, "basket retrived successfully", basket));
    }

    [HttpPost]
    public async Task<ActionResult> UpdateBasket(CustomerBasketDto basketDto)
    {
        var basket = await _basketService.UpdateBasketAsync(basketDto);
        return Ok(new ApiResponse(200, "basket updated successfully", basket));
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteBasket(string basketId)
    {
        var result = await _basketService.RemoveBasketAsync(basketId);
        return result ? Ok(new ApiResponse(200, "basket removed successfully"))
                      : BadRequest(new ApiResponse(400, "something went wrong while removing basket!"));
    }

}
