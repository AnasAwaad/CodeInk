namespace CodeInk.Core.Exceptions;
public sealed class BookNotFoundException : NotFoundException
{
    public BookNotFoundException(int id) : base($"Book with Id : {id} Not Found.")
    {

    }
}
