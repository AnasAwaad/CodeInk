using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class BookWithCategoriesSpecification : BaseSpecification<Book>
{

    // get all books
    public BookWithCategoriesSpecification() : base()
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");
    }

    //get book by id
    public BookWithCategoriesSpecification(int id) : base(b => b.Id == id)
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");
    }
}
