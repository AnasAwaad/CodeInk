using CodeInk.Service.DTOs.Payment;

namespace CodeInk.Service.DTOs.Order;
public class OrderRequestDto
{
    public ShippingAddressDto ShippingAddress { get; set; }
    public int DeliveryMethodId { get; set; }
    public IEnumerable<CartItemDto> OrderItems { get; set; } = new List<CartItemDto>();

    public string PaymentIntentId { get; set; }
    public string ClientSecret { get; set; }
}
