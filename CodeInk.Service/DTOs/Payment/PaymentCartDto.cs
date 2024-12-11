namespace CodeInk.Service.DTOs.Payment;
public class PaymentCartDto
{
    public int? DelivaryMethodId { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new();
}
