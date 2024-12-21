using CodeInk.Application.DTOs;
using CodeInk.Core.Specifications;
using CodeInk.Service.DTOs.Order;

namespace CodeInk.Service.Services.Interfaces;
public interface IOrderService
{
    Task<Pagination<OrderResultDto>> GetAllOrdersAsync(OrderSpecParams bookParams);

    Task<OrderResultDto> GetOrderByIdAsync(int id);

    Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email);

    Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRquest, string userEmail);

    Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();
}
