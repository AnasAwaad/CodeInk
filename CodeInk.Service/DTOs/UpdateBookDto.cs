using Microsoft.AspNetCore.Http;

namespace CodeInk.Application.DTOs;
public class UpdateBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public string Author { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public IFormFile CoverImage { get; set; } = null!;
    public List<int> CategoryIds { get; set; } = new List<int>();
}
