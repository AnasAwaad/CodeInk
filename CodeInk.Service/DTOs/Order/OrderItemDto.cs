namespace CodeInk.Service.DTOs.Order;
public class OrderItemDto
{
    public int BookId { get; set; }
    public string BookName { get; set; }
    public string BookPictureUrl { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
