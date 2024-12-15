using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class CategoryWithBooksSpecification : BaseSpecification<Category>
{
    public CategoryWithBooksSpecification(bool applyActiveFilteration) : base(c => !applyActiveFilteration || c.IsActive)
    {
        Includes.Add(c => c.BookCategories);
        IncludeStrings.Add("BookCategories.Book");

    }
    public CategoryWithBooksSpecification(int id, bool applyActiveFilteration) : base(c => !applyActiveFilteration || c.IsActive && c.Id == id)
    {
        Includes.Add(c => c.BookCategories);
        IncludeStrings.Add("BookCategories.Book");
    }
}
