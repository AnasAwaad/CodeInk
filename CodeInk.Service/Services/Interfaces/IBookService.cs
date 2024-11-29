using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Book;
using CodeInk.Core.Specifications;

namespace CodeInk.Core.Service;
public interface IBookService
{
    Task<Pagination<BookDetailDto>> GetBooksAsync(BookSpecParams bookParams);
    Task<BookDetailDto?> GetBookByIdAsync(int id);
    Task<ServiceResponse> CreateBookAsync(CreateBookDto bookDto);
    Task<ServiceResponse> UpdateBookAsync(UpdateBookDto bookDto);
    Task<ServiceResponse> RemoveBookAsync(int id);
}
