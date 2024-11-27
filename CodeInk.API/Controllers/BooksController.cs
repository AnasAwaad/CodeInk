using CodeInk.API.Errors;
using CodeInk.Application.DTOs;
using CodeInk.Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class BooksController : APIBaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDetailDto>>> GetBooks()
    {
        var books = await _bookService.GetBooksAsync();

        return Ok(books);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailDto>> GetBookById(int id)
    {

        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return Ok(book);
        }
        catch (KeyNotFoundException)
        {

            return NotFound(new ApiResponse(404, "Book Not Found."));
        }
    }


    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookDto bookDto)
    {
        await _bookService.CreateBookAsync(bookDto);
        return Ok(new ApiResponse(200, "Book Created Successfully"));
    }
}
