namespace CodeInk.Core.Entities.OrderEntities;
public class DeliveryMethod : BaseEntity
{
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string DeliveryTime { get; set; }// 2-3 days
    public decimal Cost { get; set; }
}
