namespace CodeInk.Core.Exceptions;
public class OrdersNotFoundException : NotFoundException
{
    public OrdersNotFoundException(string email) : base($"No orders found for the email: {email}")
    {
    }
}
