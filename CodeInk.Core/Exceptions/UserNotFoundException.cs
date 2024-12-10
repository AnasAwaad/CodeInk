namespace CodeInk.Core.Exceptions;
public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string email) : base($"User with Email : {email} Not Found.")
    {

    }
}
