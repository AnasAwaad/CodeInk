namespace CodeInk.API.DTOs;

public class BookSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string CoverImageUrl { get; set; } = null!;
}
