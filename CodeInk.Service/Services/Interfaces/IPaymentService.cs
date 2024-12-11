using CodeInk.Service.DTOs.Order;
using CodeInk.Service.DTOs.Payment;

namespace CodeInk.Service.Services.Interfaces;
public interface IPaymentService
{
    Task<PaymentCartDto> CreateOrUpdatePaymentIntent(PaymentCartDto paymentCart);

    Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId);
    Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId);
}
