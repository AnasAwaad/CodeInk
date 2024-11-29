using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class BookWithCategoriesSpecification : BaseSpecification<Book>
{

    // get all books
    public BookWithCategoriesSpecification(BookSpecParams bookParams) :
        base(b => b.IsActive && (!bookParams.CategoryId.HasValue || b.BookCategories.Any(bc => bc.CategoryId == bookParams.CategoryId)))
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");


        if (!string.IsNullOrEmpty(bookParams.OrderBy))
        {
            switch (bookParams.OrderBy)
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

        SetPagination(bookParams.PageSize * (bookParams.PageNumber - 1), bookParams.PageSize);

    }

    //get book by id
    public BookWithCategoriesSpecification(int id) : base(b => b.IsActive && b.Id == id)
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");
    }
}
