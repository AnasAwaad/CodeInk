using CodeInk.API.Errors;
using CodeInk.Application.DTOs.Book;
using CodeInk.Core.Service;
using CodeInk.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class BooksController : APIBaseController
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] BookSpecParams bookParams)
    {
        var data = await _bookService.GetAllBooksAsync(bookParams, false);
        return Ok(new ApiResponse(200, "Books retrived successfully", data));
    }

    [HttpGet("Published")]
    public async Task<IActionResult> GetPublishedBooks([FromQuery] BookSpecParams bookParams)
    {
        var data = await _bookService.GetAllBooksAsync(bookParams);
        return Ok(new ApiResponse(200, "Books retrived successfully", data));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var data = await _bookService.GetBookByIdAsync(id, false);
        return Ok(new ApiResponse(200, "Book retrived successfully", data));
    }

    [HttpGet("Published/{id}")]
    public async Task<IActionResult> GetPublishedBookById(int id)
    {
        var data = await _bookService.GetBookByIdAsync(id);
        return Ok(new ApiResponse(200, "Book retrived successfully", data));
    }

    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(10485760)]
    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookDto bookDto)
    {
        var bookId = await _bookService.CreateBookAsync(bookDto);
        return Ok(new ApiResponse(201, "Book created successfully", new { bookId }));
    }

    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(10485760)]
    [HttpPut]
    public async Task<ActionResult> UpdateBook(UpdateBookDto bookDto)
    {
        await _bookService.UpdateBookAsync(bookDto);
        return Ok(new ApiResponse(200, "Book updated successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        await _bookService.RemoveBookAsync(id);
        return Ok(new ApiResponse(200, "Book removed successfully"));
    }
}
