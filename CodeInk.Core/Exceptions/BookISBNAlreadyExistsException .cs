namespace CodeInk.Core.Exceptions;
public class BookISBNAlreadyExistsException : BadRequestException
{
    public BookISBNAlreadyExistsException(string isbn)
        : base($"A book with ISBN {isbn} already exists.")
    {
    }
}
