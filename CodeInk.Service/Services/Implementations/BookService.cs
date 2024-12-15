using AutoMapper;
using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Book;
using CodeInk.Core.Entities;
using CodeInk.Core.Exceptions;
using CodeInk.Core.Repositories;
using CodeInk.Core.Service;
using CodeInk.Core.Specifications;

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

    public async Task<Pagination<BookDetailDto>> GetAllBooksAsync(BookSpecParams bookParams, bool applyActiveFilteration = true)
    {
        var bookSpec = new BookWithCategoriesSpecification(bookParams, applyActiveFilteration);
        var books = await _bookRepo.GetAllWithSpecAsync(bookSpec);

        var mappedBooks = _mapper.Map<IReadOnlyList<BookDetailDto>>(books);

        var count = await _bookRepo.CountAllAsync();

        var totalPages = (int)Math.Ceiling(count * 1.0 / bookParams.PageSize);


        return new Pagination<BookDetailDto>
            (bookParams.PageNumber, bookParams.PageSize, totalPages, count, mappedBooks);
    }


    public async Task<BookDetailDto?> GetBookByIdAsync(int id, bool applyActiveFilteration = true)
    {
        var bookSpec = new BookWithCategoriesSpecification(id, applyActiveFilteration);
        var book = await _bookRepo.GetWithSpecAsync(bookSpec);

        if (book is null)
            throw new BookNotFoundException(id);

        return _mapper.Map<BookDetailDto>(book);
    }

    public async Task<int> CreateBookAsync(CreateBookDto bookDto)
    {
        var validateResult = ValidateBookDto(bookDto);

        if (!validateResult.isValid)
            throw new ValidationException(new List<string>() { validateResult.errorMessage });

        bool isISBNExists = await CheckIfISBNExistsAsync(bookDto.ISBN);

        if (isISBNExists)
            throw new BookISBNAlreadyExistsException(bookDto.ISBN);

        // Upload the cover image and get the URL
        string coverImageUrl = await _fileService.UploadFileAsync(bookDto.CoverImage, "Images/Books");


        var book = _mapper.Map<Book>(bookDto);
        book.CoverImageUrl = coverImageUrl;


        await AddCategoriesToBookAsync(bookDto.CategoryIds, book);

        await _bookRepo.CreateAsync(book);

        return book.Id;
    }

    public async Task UpdateBookAsync(UpdateBookDto bookDto)
    {
        var bookSpec = new BookWithCategoriesSpecification(bookDto.Id, false);
        var book = await _bookRepo.GetWithSpecAsync(bookSpec);

        if (book is null)
            throw new BookNotFoundException(bookDto.Id);

        bool isISBNExists = await CheckIfISBNExistsAsync(bookDto.ISBN);

        if (isISBNExists && book.ISBN != bookDto.ISBN)
            throw new BookISBNAlreadyExistsException(book.ISBN);

        book = _mapper.Map(bookDto, book);

        if (bookDto.CoverImage is not null)
        {
            _fileService.DeleteFile(book.CoverImageUrl);

            // Upload the cover image and get the URL
            string coverImageUrl = await _fileService.UploadFileAsync(bookDto.CoverImage, "Images/Books");

            book.CoverImageUrl = coverImageUrl;
        }

        // remove old categories for book and then add new categories to book
        book.BookCategories.Clear();
        await AddCategoriesToBookAsync(bookDto.CategoryIds, book);
        book.LastUpdatedOn = DateTime.Now;
        await _bookRepo.UpdateAsync(book);
    }

    public async Task RemoveBookAsync(int id)
    {
        var spec = new BookByIdSpecification(id);

        var book = await _bookRepo.GetWithSpecAsync(spec);

        if (book is null)
            throw new BookNotFoundException(id);

        book.IsActive = false;
        await _bookRepo.UpdateAsync(book);
    }


    #region Private methods
    private async Task<bool> CheckIfISBNExistsAsync(string isbn)
    {
        var spec = new BookISBNExistsSpecification(isbn);
        return await _bookRepo.IsExistsWithSpecAsync(spec);
    }

    private async Task AddCategoriesToBookAsync(List<int> categoryIds, Book book)
    {
        var categorySpec = new CategoriesByIdsWithSpecification(categoryIds);
        var categories = await _categoryRepo.GetAllWithSpecAsync(categorySpec);


        foreach (var category in categories)
        {
            book.BookCategories.Add(new BookCategory
            {
                BookId = book.Id,
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
    #endregion

}
