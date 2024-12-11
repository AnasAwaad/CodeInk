using CodeInk.API.Errors;
using CodeInk.Service.DTOs.Payment;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class PaymentsController : APIBaseController
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrUpdatePaymentIntent(PaymentCartDto paymentCart)
    {
        var result = await _paymentService.CreateOrUpdatePaymentIntent(paymentCart);
        return Ok(new ApiResponse(201, "Payment intent created successfully", result));
    }
}
