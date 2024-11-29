using AutoMapper;
using CodeInk.Application.DTOs;
using CodeInk.Application.DTOs.Book;
using CodeInk.Core.Entities;
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

    public async Task<Pagination<BookDetailDto>> GetBooksAsync(BookSpecParams bookParams)
    {
        var bookSpec = new BookWithCategoriesSpecification(bookParams);
        var books = await _bookRepo.GetAllWithSpecAsync(bookSpec);

        var mappedBooks = _mapper.Map<IReadOnlyList<BookDetailDto>>(books);

        var activeBookSpec = new ActiveBooksSpecification();
        var count = await _bookRepo.CountWithSpecAsync(activeBookSpec);

        var totalPages = (int)Math.Ceiling(count * 1.0 / bookParams.PageSize);


        return new Pagination<BookDetailDto>
            (bookParams.PageNumber, bookParams.PageSize, totalPages, count, mappedBooks);
    }

    public async Task<BookDetailDto?> GetBookByIdAsync(int id)
    {
        var bookSpec = new BookWithCategoriesSpecification(id);
        var book = await _bookRepo.GetByIdWithSpecAsync(bookSpec);

        if (book is null)
            return null;

        return _mapper.Map<BookDetailDto>(book);
    }

    public async Task<ServiceResponse> CreateBookAsync(CreateBookDto bookDto)
    {
        var validateResult = ValidateBookDto(bookDto);

        if (!validateResult.isValid)
            return new ServiceResponse(false, validateResult.errorMessage);

        bool isISBNExists = await CheckIfISBNExistsAsync(bookDto.ISBN);
        if (isISBNExists)
            return new ServiceResponse(false, "A book with this ISBN already exists.", ServiceErrorCode.BadRequest);

        // Upload the cover image and get the URL
        string coverImageUrl = await _fileService.UploadFileAsync(bookDto.CoverImage, "Images/Books");


        var book = _mapper.Map<Book>(bookDto);
        book.CoverImageUrl = coverImageUrl;


        await AddCategoriesToBookAsync(bookDto.CategoryIds, book);

        await _bookRepo.CreateAsync(book);

        return new ServiceResponse(true, "Book created successfully", ServiceErrorCode.Created);
    }

    public async Task<ServiceResponse> UpdateBookAsync(UpdateBookDto bookDto)
    {
        var bookSpec = new BookWithCategoriesSpecification(bookDto.Id);
        var book = await _bookRepo.GetByIdWithSpecAsync(bookSpec);

        if (book is null)
            return new ServiceResponse(false, $"Book with Id {bookDto.Id} not found.", ServiceErrorCode.NotFound);

        bool isISBNExists = await CheckIfISBNExistsAsync(bookDto.ISBN);

        if (isISBNExists && book.ISBN != bookDto.ISBN)
            return new ServiceResponse(false, "A book with this ISBN already exists.", ServiceErrorCode.BadRequest);

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

        await _bookRepo.UpdateAsync(book);

        return new ServiceResponse(true, "Book updated successfully.", ServiceErrorCode.Success);
    }



    public async Task<ServiceResponse> RemoveBookAsync(int id)
    {
        var spec = new BookByIdSpecification(id);

        var book = await _bookRepo.GetByIdWithSpecAsync(spec);

        if (book is null)
            return new ServiceResponse(false, $"Book with Id {id} Not Found", ServiceErrorCode.NotFound);

        book.IsActive = false;
        await _bookRepo.UpdateAsync(book);

        return new ServiceResponse(true, "Book removed successfully", ServiceErrorCode.Success);
    }



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


}
