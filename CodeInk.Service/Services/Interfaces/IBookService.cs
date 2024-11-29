using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Book;
using CodeInk.Core.Specifications;

namespace CodeInk.Core.Service;
public interface IBookService
{
    public Task<Pagination<BookDetailDto>> GetBooksAsync(BookSpecParams bookParams);
    public Task<BookDetailDto?> GetBookByIdAsync(int id);
    public Task<ServiceResponse> CreateBookAsync(CreateBookDto bookDto);
    public Task<ServiceResponse> UpdateBookAsync(UpdateBookDto bookDto);
}
