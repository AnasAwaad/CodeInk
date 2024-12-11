using CodeInk.Service.DTOs.Payment;
using CodeInk.Service.DTOs.User;

namespace CodeInk.Service.DTOs.Order;
public class OrderResultDto
{
    public int Id { get; set; }
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string PaymentStatus { get; set; }
    public AddressDto ShippingAddress { get; set; }
    public string DeliveryMethod { get; set; }
    public decimal DeliveryMethodCost { get; set; }
    public ICollection<CartItemDto> OrderItems { get; set; } = new HashSet<CartItemDto>();
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string PaymentIntentId { get; set; }
}
