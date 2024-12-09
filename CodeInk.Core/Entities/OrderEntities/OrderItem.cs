namespace CodeInk.Core.Entities.OrderEntities;
public class OrderItem : BaseEntity
{
    public int BookId { get; set; }
    public string BookName { get; set; }
    public string BookPictureUrl { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
