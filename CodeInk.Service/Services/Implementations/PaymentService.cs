using AutoMapper;
using CodeInk.Core.Entities;
using CodeInk.Core.Entities.OrderEntities;
using CodeInk.Core.Exceptions;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;
using CodeInk.Service.DTOs.Order;
using CodeInk.Service.DTOs.Payment;
using CodeInk.Service.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace CodeInk.Service.Services.Implementations;
public class PaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly IGenericRepository<DeliveryMethod> _deliveryMethod;
    private readonly IGenericRepository<Book> _bookRepo;
    private readonly IGenericRepository<Order> _orderRepo;
    private readonly IMapper _mapper;

    public PaymentService(IConfiguration configuration, IGenericRepository<DeliveryMethod> deliveryMethod, IGenericRepository<Order> orderRepo, IMapper mapper, IGenericRepository<Book> bookRepo)
    {
        _configuration = configuration;
        _deliveryMethod = deliveryMethod;
        _orderRepo = orderRepo;
        _mapper = mapper;
        _bookRepo = bookRepo;
    }

    public async Task<PaymentCartDto> CreateOrUpdatePaymentIntent(PaymentCartDto paymentCart)
    {
        StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

        var totalPrice = 0m;
        if (paymentCart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await _deliveryMethod.GetByIdAsync(paymentCart.DeliveryMethodId.Value)
                    ?? throw new DeliveryMethodNotFoundException(paymentCart.DeliveryMethodId.Value);

            totalPrice = deliveryMethod.Price;
            paymentCart.ShippingPrice = deliveryMethod.Price;
        }

        if (paymentCart.CartItems.Any())
        {
            foreach (var item in paymentCart.CartItems)
            {
                var book = await _bookRepo.GetByIdAsync(item.BookId)
                    ?? throw new BookNotFoundException(item.BookId);

                item.Price = book.Price;
                totalPrice += (book.Price * item.Quantity);
            }
        }

        var service = new PaymentIntentService();
        PaymentIntent paymentIntent;

        if (string.IsNullOrEmpty(paymentCart.PaymentIntentId))
        {
            // create payment intent id

            var options = new PaymentIntentCreateOptions()
            {
                Amount = (long)totalPrice * 100,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" }
            };

            paymentIntent = await service.CreateAsync(options);

            paymentCart.PaymentIntentId = paymentIntent.Id;
            paymentCart.ClientSecret = paymentIntent.ClientSecret;
        }
        else
        {
            // update amount of payment 
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)totalPrice * 100,
            };

            await service.UpdateAsync(paymentCart.PaymentIntentId, options);

        }

        return paymentCart;
    }

    public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
    {
        var order = await _orderRepo.GetWithSpecAsync(new OrderByPaymentIntentIdSpecification(paymentIntentId))
                    ?? throw new OrderNotFoundException(paymentIntentId);

        order.Status = OrderStatus.PaymentFailed;

        await _orderRepo.UpdateAsync(order);

        return _mapper.Map<OrderResultDto>(order);

    }

    public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
    {
        var order = await _orderRepo.GetWithSpecAsync(new OrderByPaymentIntentIdSpecification(paymentIntentId))
                    ?? throw new OrderNotFoundException(paymentIntentId);

        order.Status = OrderStatus.PaymentReceived;

        await _orderRepo.UpdateAsync(order);

        return _mapper.Map<OrderResultDto>(order);
    }
}
