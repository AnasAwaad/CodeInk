namespace CodeInk.Core.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<BookCategory> BookCategories { get; set; }
}
