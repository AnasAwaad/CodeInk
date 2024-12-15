namespace CodeInk.Application.DTOs.Category;

public class CategoryToReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int NumOfBooks { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastUpdatedOn { get; set; }

}
