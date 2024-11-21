namespace CodeInk.Core.Entities;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public string CoverImageUrl { get; set; }
    public ICollection<BookCategory> BookCategories { get; set; }
}
