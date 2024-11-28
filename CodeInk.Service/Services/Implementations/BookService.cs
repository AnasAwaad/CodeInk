using AutoMapper;
using CodeInk.Application.DTOs;
using CodeInk.Core.Entities;
using CodeInk.Core.Repositories;
using CodeInk.Core.Service;
using CodeInk.Core.Specifications;
using Microsoft.AspNetCore.Http;

namespace CodeInk.Application.Services.Implementations;
public class BookService : IBookService
{
    private readonly IGenericRepository<Book> _bookRepo;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;
    private readonly IGenericRepository<Category> _categoryRepo;

    public BookService(IGenericRepository<Book> bookRepo, IMapper mapper, IFileService fileService, IGenericRepository<Category> categoryRepo)
    {
        _bookRepo = bookRepo;
        _mapper = mapper;
        _fileService = fileService;
        _categoryRepo = categoryRepo;
    }

    public async Task<ApiResponse> GetBooksAsync(BookSpecParams bookParams)
    {
        var bookSpec = new BookWithCategoriesSpecification(bookParams);
        var books = await _bookRepo.GetAllWithSpecAsync(bookSpec);

        var mappedBooks = _mapper.Map<IReadOnlyList<BookDetailDto>>(books);


        var count = await _bookRepo.CountAllAsync();
        var totalPages = (int)Math.Ceiling(count * 1.0 / bookParams.PageSize);

        var paginatedBooks = new Pagination<BookDetailDto>(bookParams.PageNumber, bookParams.PageSize, totalPages, count, mappedBooks);
        return new ApiResponse(200, "Books retrieved successfully.", paginatedBooks);

    }

    public async Task<ApiResponse> GetBookByIdAsync(int id)
    {
        var bookSpec = new BookWithCategoriesSpecification(id);
        var book = await _bookRepo.GetByIdWithSpecAsync(bookSpec);

        if (book is null)
            return new ApiResponse(404, $"Book with Id {id} Not Found");

        var mappedBook = _mapper.Map<BookDetailDto>(book);

        return new ApiResponse(200, "Books retrived successfully", mappedBook);
    }

    public async Task<ApiResponse> CreateBookAsync(CreateBookDto bookDto)
    {
        var validateResult = ValidateBookDto(bookDto);

        if (!validateResult.isValid)
            return new ApiResponse(400, validateResult.errorMessage);

        bool isISBNExists = await CheckIfISBNExistsAsync(bookDto.ISBN);
        if (isISBNExists)
            return new ApiResponse(400, "A book with this ISBN already exists.");

        // Upload the cover image and get the URL
        string coverImageUrl = await UploadCoverImageAsync(bookDto.CoverImage);


        var book = _mapper.Map<Book>(bookDto);
        book.CoverImageUrl = coverImageUrl;


        await AddCategoriesToBookAsync(bookDto.CategoryIds, book);

        await _bookRepo.CreateAsync(book);

        return new ApiResponse(201, "Book created successfully");
    }

    private async Task<bool> CheckIfISBNExistsAsync(string isbn)
    {
        var spec = new BookISBNExistsSpecification(isbn);
        return await _bookRepo.IsExistsWithSpecAsync(spec);
    }

    private async Task<string> UploadCoverImageAsync(IFormFile coverImage)
    {
        if (coverImage == null)
            throw new ArgumentNullException(nameof(coverImage), "Cover image must be provided.");
        return await _fileService.UploadFileAsync(coverImage, "Images/Books");
    }

    private async Task AddCategoriesToBookAsync(List<int> categoryIds, Book book)
    {
        var categorySpec = new CategoriesByIdsWithSpecification(categoryIds);
        var categories = await _categoryRepo.GetAllWithSpecAsync(categorySpec);

        foreach (var category in categories)
        {
            book.BookCategories.Add(new BookCategory
            {
                CategoryId = category.Id
            });
        }
    }

    private (bool isValid, string errorMessage) ValidateBookDto(CreateBookDto bookDto)
    {
        if (bookDto.CategoryIds == null || !bookDto.CategoryIds.Any())
            return (false, "At least one category must be selected.");
        return (true, string.Empty);
    }
}
