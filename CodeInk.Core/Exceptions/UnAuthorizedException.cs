namespace CodeInk.Core.Exceptions;
public sealed class UnAuthorizedException : Exception
{
    public UnAuthorizedException(string message) : base(message)
    {

    }
}
