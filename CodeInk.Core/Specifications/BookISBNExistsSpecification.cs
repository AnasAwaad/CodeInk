using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class BookISBNExistsSpecification : BaseSpecification<Book>
{
    public BookISBNExistsSpecification()
    {

    }

    public BookISBNExistsSpecification(string isbn) : base(b => b.ISBN == isbn)
    {

    }
}
