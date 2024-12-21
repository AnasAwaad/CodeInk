using CodeInk.Core.Entities.OrderEntities;

namespace CodeInk.Core.Specifications;
public class OrderWithIncludesSpecification : BaseSpecification<Order>
{
    public OrderWithIncludesSpecification(int id) : base(o => o.Id == id)
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.OrderItems);
    }

    public OrderWithIncludesSpecification(string email) : base(o => o.BuyerEmail == email)
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.OrderItems);

        SetOrderBy(o => o.OrderDate);
    }
    public OrderWithIncludesSpecification() : base(o => o.IsActive)
    {
        Includes.Add(o => o.DeliveryMethod);
        Includes.Add(o => o.OrderItems);

        SetOrderBy(o => o.OrderDate);
    }
}
