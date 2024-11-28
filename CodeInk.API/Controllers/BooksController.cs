using CodeInk.Application.DTOs;
using CodeInk.Core.Service;
using CodeInk.Core.Specifications;
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
    public async Task<ActionResult<IEnumerable<BookDetailDto>>> GetBooks([FromQuery] BookSpecParams bookParams)
    {
        var response = await _bookService.GetBooksAsync(bookParams);

        return StatusCode(response.StatusCode, response);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailDto>> GetBookById(int id)
    {
        var response = await _bookService.GetBookByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }


    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookDto bookDto)
    {
        var response = await _bookService.CreateBookAsync(bookDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBook(UpdateBookDto bookDto)
    {
        var response = await _bookService.UpdateBookAsync(bookDto);
        return StatusCode(response.StatusCode, response);
    }
}
