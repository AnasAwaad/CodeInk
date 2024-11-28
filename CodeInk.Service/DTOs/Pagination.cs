namespace CodeInk.Application.DTOs;
public class Pagination<T>
{
    public Pagination(int pageNumber, int pageSize, int totalPages, int count, IReadOnlyList<T> items)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
        Count = count;
        Items = items;
    }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int Count { get; set; }
    public IReadOnlyList<T> Items { get; set; }
}
