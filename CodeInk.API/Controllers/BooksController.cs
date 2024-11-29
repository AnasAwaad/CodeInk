using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Book;
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
    public async Task<IActionResult> GetBooks([FromQuery] BookSpecParams bookParams)
    {
        var data = await _bookService.GetBooksAsync(bookParams);

        return Ok(new ApiResponse(200, "Books retrived successfully", data));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var data = await _bookService.GetBookByIdAsync(id);

        return data is null ? NotFound(new ApiResponse(404, $"Book with Id {id} Not Found"))
                              : Ok(new ApiResponse(200, "Book retrived successfully", data));
    }


    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookDto bookDto)
    {
        var result = await _bookService.CreateBookAsync(bookDto);

        return result.success ? Ok(new ApiResponse(201, result.message))
                              : BadRequest(new ApiResponse(300, result.message));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateBook(UpdateBookDto bookDto)
    {
        var result = await _bookService.UpdateBookAsync(bookDto);

        return result.success ? Ok(new ApiResponse(201, result.message))
                              : BadRequest(new ApiResponse(300, result.message));
    }
}
