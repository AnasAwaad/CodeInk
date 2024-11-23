using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.Core.Entities;

namespace CodeInk.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

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
