namespace CodeInk.API.DTOs;

public class CategoryToReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BookToReturnDto> Books { get; set; }

}
