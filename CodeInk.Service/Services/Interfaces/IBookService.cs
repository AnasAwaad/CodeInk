using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Book;
using CodeInk.Core.Specifications;

namespace CodeInk.Core.Service;
public interface IBookService
{
    Task<Pagination<BookDetailDto>> GetAllBooksAsync(BookSpecParams bookParams, bool publishedOnly = true);
    Task<BookDetailDto?> GetBookByIdAsync(int id, bool publishedOnly = true);
    Task<int> CreateBookAsync(CreateBookDto bookDto);
    Task UpdateBookAsync(UpdateBookDto bookDto);
    Task RemoveBookAsync(int id);
}
