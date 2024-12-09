namespace CodeInk.Core.Entities.OrderEntities;
public class DeliveryMethod : BaseEntity
{
    public DeliveryMethod()
    {

    }
    public DeliveryMethod(string shortName, string description, string deliveryTime, decimal price)
    {
        ShortName = shortName;
        Description = description;
        DeliveryTime = deliveryTime;
        Price = price;
    }

    public string ShortName { get; set; }
    public string Description { get; set; }
    public string DeliveryTime { get; set; }// 2-3 days
    public decimal Price { get; set; }
}
