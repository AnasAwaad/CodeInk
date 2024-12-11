namespace CodeInk.Service.DTOs.Payment;
public class CartItemDto
{
    public int BookId { get; set; }
    public string BookTitle { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
