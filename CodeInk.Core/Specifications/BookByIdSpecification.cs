using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class BookByIdSpecification : BaseSpecification<Book>
{
    public BookByIdSpecification(int id) : base(b => b.Id == id)
    {

    }
}
