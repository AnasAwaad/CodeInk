using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class ActiveBooksSpecification : BaseSpecification<Book>
{
    public ActiveBooksSpecification() : base(b => b.IsActive)
    {

    }
}
