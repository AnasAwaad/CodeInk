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

    public async Task<IEnumerable<BookDetailDto>> GetBooksAsync()
    {
        var bookSpec = new BookWithCategoriesSpecification();
        var books = await _bookRepo.GetAllWithSpecAsync(bookSpec);

        return _mapper.Map<IEnumerable<BookDetailDto>>(books);
    }

    public async Task<BookDetailDto> GetBookByIdAsync(int id)
    {
        var bookSpec = new BookWithCategoriesSpecification(id);
        var book = await _bookRepo.GetByIdWithSpecAsync(bookSpec);

        if (book is null)
            throw new KeyNotFoundException("Book Not Found.");

        return _mapper.Map<BookDetailDto>(book);
    }

    public async Task CreateBookAsync(CreateBookDto bookDto)
    {
        // Validate input (ensure categories are selected)
        ValidateBookDto(bookDto);

        // Check if the ISBN already exists
        bool isISBNExists = await CheckIfISBNExistsAsync(bookDto.ISBN);
        if (isISBNExists)
            throw new InvalidOperationException("A book with this ISBN already exists.");

        // Upload the cover image and get the URL
        string coverImageUrl = await UploadCoverImageAsync(bookDto.CoverImage);

        // Map DTO to Book entity
        var book = _mapper.Map<Book>(bookDto);
        book.CoverImageUrl = coverImageUrl;

        // Add categories to the book
        await AddCategoriesToBookAsync(bookDto.CategoryIds, book);

        // Save the book to the repository
        await _bookRepo.CreateAsync(book);
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

    private void ValidateBookDto(CreateBookDto bookDto)
    {
        if (bookDto.CategoryIds == null || !bookDto.CategoryIds.Any())
            throw new ArgumentException("At least one category must be selected.");
    }
}
