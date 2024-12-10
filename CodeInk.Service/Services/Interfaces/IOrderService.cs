using CodeInk.Service.DTOs.Order;

namespace CodeInk.Service.Services.Interfaces;
public interface IOrderService
{

    Task<OrderResultDto> GetOrderByIdAsync(int id);

    Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email);

    Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRquest, string userEmail);

    Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();
}
