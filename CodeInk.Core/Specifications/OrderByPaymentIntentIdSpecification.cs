using CodeInk.Core.Entities.OrderEntities;

namespace CodeInk.Core.Specifications;
public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
{
    public OrderByPaymentIntentIdSpecification(string paymentIntentId)
        : base(o => o.IsActive && o.PaymentIntentId == paymentIntentId)
    {
        Includes.Add(o => o.OrderItems);
        Includes.Add(o => o.DeliveryMethod);
    }
}
