using CodeInk.Core.Entities;

namespace CodeInk.Core.Specifications;
public class BookWithCategoriesSpecification : BaseSpecification<Book>
{

    // get all books
    // get all published books only if publishedOnly flag is true
    // get all published and unPublished books if publishedOnly flag is false
    // and get all active books (not deleted) (soft delete)
    public BookWithCategoriesSpecification(BookSpecParams bookParams, bool publishedOnly) :
        base(b => (!publishedOnly || b.IsPublished) && b.IsActive && (!bookParams.CategoryId.HasValue || b.BookCategories.Any(bc => bc.CategoryId == bookParams.CategoryId)))
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
    public BookWithCategoriesSpecification(int id, bool publishedOnly) :
        base(b => (!publishedOnly || b.IsPublished) && b.IsActive && (b.Id) == id)
    {
        Includes.Add(b => b.BookCategories);
        IncludeStrings.Add("BookCategories.Category");
    }
}
