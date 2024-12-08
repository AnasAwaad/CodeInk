namespace CodeInk.Core.Exceptions;
public class ValidationException : Exception
{
    public IEnumerable<string> Errors { get; set; }
    public ValidationException(IEnumerable<string> errros) : base("Validation Field")
    {
        Errors = errros;
    }
}
