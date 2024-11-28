using CodeInk.Application.DTOs;
using CodeInk.Core.Specifications;

namespace CodeInk.Core.Service;
public interface IBookService
{
    public Task<ApiResponse> GetBooksAsync(BookSpecParams bookParams);
    public Task<ApiResponse> GetBookByIdAsync(int id);
    public Task<ApiResponse> CreateBookAsync(CreateBookDto bookDto);
    public Task<ApiResponse> UpdateBookAsync(UpdateBookDto bookDto);
}
