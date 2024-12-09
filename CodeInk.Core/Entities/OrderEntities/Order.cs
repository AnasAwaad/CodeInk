namespace CodeInk.Core.Entities.OrderEntities;
public class Order : BaseEntity
{
    public string BuyerEmail { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public Address ShippingAddress { get; set; }
    public int DeliveryMethodId { get; set; } // FK
    public DeliveryMethod DeliveryMethod { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
    public decimal SubTotal { get; set; }
    public string PaymentIntentId { get; set; } = string.Empty;
    public decimal GetTotal
        => SubTotal + DeliveryMethod.Cost;


}
