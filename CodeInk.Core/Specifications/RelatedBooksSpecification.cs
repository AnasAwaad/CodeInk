using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class RelatedBooksSpecification : BaseSpecification<Book>
{
    public RelatedBooksSpecification(int bookId, List<int> categoryIds) : base(b => b.Id != bookId && b.BookCategories.Any(bc => categoryIds.Contains(bc.CategoryId)))
    {
        Includes.Add(b => b.BookCategories);
    }
}
