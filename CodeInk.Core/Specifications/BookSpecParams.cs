namespace CodeInk.Core.Specifications;
public class BookSpecParams
{
    public string? OrderBy { get; set; }

    private int pageSize = 12;
    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value > 24 ? 24 : value; }
    }

    public int PageNumber { get; set; } = 1;
}
