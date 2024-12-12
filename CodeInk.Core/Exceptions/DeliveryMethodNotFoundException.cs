namespace CodeInk.Core.Exceptions;
public class DeliveryMethodNotFoundException : NotFoundException
{
    public DeliveryMethodNotFoundException(int id) : base($"Delivery method with Id : {id} Not Found.")
    {

    }
}
