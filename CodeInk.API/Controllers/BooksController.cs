using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.API.Errors;
using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class BooksController : APIBaseController
{
    private readonly IGenericRepository<Book> _bookRepo;
    private readonly IMapper _mapper;

    public BooksController(IGenericRepository<Book> bookRepo, IMapper mapper)
    {
        _bookRepo = bookRepo;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDetailDto>>> GetBooks()
    {
        var bookSpec = new BookWithCategoriesSpecification();
        var books = await _bookRepo.GetAllWithSpecAsync(bookSpec);
        var mappedBooks = _mapper.Map<IEnumerable<BookDetailDto>>(books);

        return Ok(mappedBooks);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetailDto>> GetBookById(int id)
    {

        var bookSpec = new BookWithCategoriesSpecification(id);
        var book = await _bookRepo.GetByIdWithSpecAsync(bookSpec);

        if (book is null)
            return NotFound(new ApiResponse(404));

        var mappedBook = _mapper.Map<BookDetailDto>(book);

        return Ok(mappedBook);
    }
}
