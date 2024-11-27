using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class BookWithCategoriesSpecification : BaseSpecification<Book>
{

    // get all books
    public BookWithCategoriesSpecification(string? OrderBy) : base()
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");

        if (!string.IsNullOrEmpty(OrderBy))
        {
            switch (OrderBy)
            {
                case "PriceAsc":
                    SetOrderBy(b => b.Price);
                    break;
                case "PriceDesc":
                    SetOrderByDesc(b => b.Price);
                    break;
                default:
                    SetOrderBy(b => b.Title);
                    break;
            }
        }

    }

    //get book by id
    public BookWithCategoriesSpecification(int id) : base(b => b.Id == id)
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");
    }
}
