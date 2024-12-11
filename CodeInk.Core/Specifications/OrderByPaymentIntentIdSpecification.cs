using CodeInk.Core.Entities.OrderEntities;

namespace CodeInk.Core.Specifications;
public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
{
    public OrderByPaymentIntentIdSpecification(string paymentIntentId)
        : base(o => o.PaymentIntentId == paymentIntentId)
    {

    }
}
