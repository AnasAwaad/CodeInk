using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class CategoryWithBooksSpecification : BaseSpecification<Category>
{
    public CategoryWithBooksSpecification() : base(c => c.IsActive)
    {
        Includes.Add(c => c.BookCategories);
        IncludeStrings.Add("BookCategories.Book");

    }
    public CategoryWithBooksSpecification(int id) : base(c => c.IsActive && c.Id == id)
    {
        Includes.Add(c => c.BookCategories);
        IncludeStrings.Add("BookCategories.Book");
    }
}
