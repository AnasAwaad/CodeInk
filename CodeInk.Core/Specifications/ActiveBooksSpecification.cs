using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class ActiveBooksSpecification : BaseSpecification<Book>
{
    public ActiveBooksSpecification(bool publishedOnly) : base(b => (!publishedOnly || b.IsPublished) && b.IsActive)
    {

    }
}
