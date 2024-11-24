using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.API.Errors;
using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Service;
using CodeInk.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Controllers;

public class BooksController : APIBaseController
{
    private readonly IGenericRepository<Book> _bookRepo;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly ICategoryRepository _categoryRepo;

    public BooksController(IGenericRepository<Book> bookRepo, IMapper mapper, IFileService fileService, ICategoryRepository categoryRepo)
    {
        _bookRepo = bookRepo;
        _mapper = mapper;
        _fileService = fileService;
        _categoryRepo = categoryRepo;
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


    [HttpPost]
    public async Task<ActionResult> CreateBook(CreateBookDto bookDto)
    {
        if (bookDto.CategoryIds is null || bookDto.CategoryIds.Count == 0)
            throw new ArgumentException("At least one category must be selected.");


        var spec = new BookISBNExistsSpecification(bookDto.ISBN);
        bool isISBNExists = await _bookRepo.IsExistsWithSpecAsync(spec);

        if (isISBNExists)
        {
            throw new InvalidOperationException("A book with this ISBN already exists.");
        }


        string coverImageUrl = await _fileService.UploadFileAsync(bookDto.CoverImage, "Images/Books");


        var book = _mapper.Map<Book>(bookDto);

        book.CoverImageUrl = coverImageUrl;

        var categories = await _categoryRepo.GetByIdsAsync(bookDto.CategoryIds);

        foreach (var category in categories)
        {
            book.BookCategories.Add(new BookCategory
            {
                CategoryId = category.Id
            });
        }


        await _bookRepo.CreateAsync(book);

        return Ok(new ApiResponse(200, "Book Created Successfully"));
    }
}
