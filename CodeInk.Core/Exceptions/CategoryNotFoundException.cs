namespace CodeInk.Core.Exceptions;
public class CategoryNotFoundException : NotFoundException
{
    public CategoryNotFoundException(int id) : base($"Category with Id : {id} Not Found.")
    {

    }
}
