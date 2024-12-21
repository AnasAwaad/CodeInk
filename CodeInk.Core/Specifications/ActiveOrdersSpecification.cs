using CodeInk.Core.Entities.OrderEntities;

namespace CodeInk.Core.Specifications;
public class ActiveOrdersSpecification : BaseSpecification<Order>
{
    public ActiveOrdersSpecification() : base(o => o.IsActive)
    {

    }
}
