using CodeInk.Service.DTOs.Order;

namespace CodeInk.Service.Services.Interfaces;
public interface IOrderService
{
    // Get order by id => OrderResultDto(Guid id)
    // Get orders for user by email=> IEnumerable<OrderResultDto> (string email)
    // create order => OrderResult(CreateOrderDto,email)
    Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRquest, string userEmail);
    // get all delivery methods
}
