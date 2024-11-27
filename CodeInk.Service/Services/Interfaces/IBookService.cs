using CodeInk.Application.DTOs;

namespace CodeInk.Core.Service;
public interface IBookService
{
    public Task<IEnumerable<BookDetailDto>> GetBooksAsync();
    public Task<BookDetailDto> GetBookByIdAsync(int id);
    public Task CreateBookAsync(CreateBookDto bookDto);
}
