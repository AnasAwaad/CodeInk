namespace CodeInk.Repository.Models;

public class CustomerBasket
{
    public string Id { get; set; }
    public int? DelivaryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}
