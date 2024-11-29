using CodeInk.Application.DTOs.Category;

namespace CodeInk.Application.DTOs.Book;

public class BookDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string CoverImageUrl { get; set; } = null!;
    public ICollection<BookCategoriesDto> Categories { get; set; }
}
