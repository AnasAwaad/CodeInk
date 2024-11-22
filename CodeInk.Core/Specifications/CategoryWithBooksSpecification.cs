using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class CategoryWithBooksSpecification : BaseSpecification<Category>
{
    public CategoryWithBooksSpecification(int id) : base(c => c.Id == id)
    {
        //Includes.Add(c => c.BookCategories);
    }
}
