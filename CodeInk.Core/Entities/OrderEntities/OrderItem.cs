namespace CodeInk.Core.Entities.OrderEntities;
public class OrderItem : BaseEntity
{
    public OrderItem()
    {

    }
    public OrderItem(int bookId, string bookName, string bookPictureUrl, int quantity, decimal price)
    {
        BookId = bookId;
        BookName = bookName;
        BookPictureUrl = bookPictureUrl;
        Quantity = quantity;
        Price = price;
    }

    public int BookId { get; set; }
    public string BookName { get; set; }
    public string BookPictureUrl { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
