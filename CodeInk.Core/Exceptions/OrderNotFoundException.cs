namespace CodeInk.Core.Exceptions;
public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(int id) : base($"Order with Id : {id} Not Found.")
    {

    }
}
