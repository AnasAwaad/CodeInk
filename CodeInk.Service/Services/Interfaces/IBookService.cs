using CodeInk.Application.DTOs;

namespace CodeInk.Core.Service;
public interface IBookService
{
    public Task<ApiResponse> GetBooksAsync(string? Sort);
    public Task<ApiResponse> GetBookByIdAsync(int id);
    public Task<ApiResponse> CreateBookAsync(CreateBookDto bookDto);
}
