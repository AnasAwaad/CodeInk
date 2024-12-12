using CodeInk.API.Errors;
using CodeInk.Service.DTOs.Payment;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CodeInk.API.Controllers;

public class PaymentsController : APIBaseController
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentsController> _logger;

    public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }


    [HttpPost]
    [Authorize(Roles = "Customer")]

    public async Task<IActionResult> CreateOrUpdatePaymentIntent(PaymentCartDto paymentCart)
    {
        var result = await _paymentService.CreateOrUpdatePaymentIntent(paymentCart);
        return Ok(new ApiResponse(201, "Payment intent created successfully", result));
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebHook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        const string endpointSecret = "whsec_34d7d3c08a44698b2cfda012f1fe3a2c971e73be8acd05195e411fdae6dbe5ae";

        var stripeEvent = EventUtility.ParseEvent(json);
        var signatureHeader = Request.Headers["Stripe-Signature"];

        stripeEvent = EventUtility.ConstructEvent(json,
                signatureHeader, endpointSecret);

        if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            _logger.Log(LogLevel.Information, "Payment completed successfully ya anas", paymentIntent.Id);
            await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);

        }
        else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            _logger.Log(LogLevel.Information, "Payment failed ya anas :)", paymentIntent.Id);

            await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
        }
        else if (stripeEvent.Type == EventTypes.PaymentIntentCreated)
        {
            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            _logger.Log(LogLevel.Information, "Payment intent id created ya anas", paymentIntent.Id);

        }

        return new EmptyResult();

    }
}
