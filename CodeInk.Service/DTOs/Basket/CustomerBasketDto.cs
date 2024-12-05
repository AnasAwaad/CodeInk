namespace CodeInk.Service.DTOs.Basket;
public class CustomerBasketDto
{
    public string? Id { get; set; }
    public int? DelivaryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
}
