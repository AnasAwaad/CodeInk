namespace CodeInk.Core.Entities;
public class Book : BaseEntity
{
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string CoverImageUrl { get; set; } = null!;
    public ICollection<BookCategory> BookCategories { get; set; }
}
