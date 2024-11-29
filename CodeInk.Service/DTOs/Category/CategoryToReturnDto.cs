using CodeInk.Application.DTOs.Book;

namespace CodeInk.Application.DTOs.Category;

public class CategoryToReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BookSummaryDto> Books { get; set; }

}
