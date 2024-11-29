using AutoMapper;
using CodeInk.Application.DTOs.Book;
using CodeInk.Application.DTOs.Category;
using CodeInk.Application.Mapping.Resolvers;
using CodeInk.Core.Entities;

namespace CodeInk.Application.Mapping;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateBookDto, Book>();
        CreateMap<UpdateBookDto, Book>();
        CreateMap<Book, BookSummaryDto>()
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom<BookPictureUrlResolver>());

        CreateMap<Book, BookDetailDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category)))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom<BookPictureUrlResolver>());

        CreateMap<Category, BookCategoriesDto>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<AddCategoryDto, Category>();

        CreateMap<Category, CategoryToReturnDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(s => s.BookCategories.Select(bc => bc.Book)));
    }
}
