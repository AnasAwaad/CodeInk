namespace CodeInk.Service.DTOs.Payment;
public class PaymentCartDto
{
    public int? DeliveryMethodId { get; set; }
    public decimal? ShippingPrice { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
}
