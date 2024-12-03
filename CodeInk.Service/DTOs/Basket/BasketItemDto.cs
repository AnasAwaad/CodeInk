namespace CodeInk.Service.DTOs.Basket;
public class BasketItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
