namespace CodeInk.Core.Exceptions;
public class CategoryNameAlreadyExistsException : BadRequestException
{
    public CategoryNameAlreadyExistsException(string name)
    : base($"A Category with Name : {name} already exists.")
    {
    }
}
