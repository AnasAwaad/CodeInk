using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.Core.Entities;

namespace CodeInk.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<Book, BookToReturnDto>()
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom<BookPictureUrlResolver>());


        CreateMap<Category, CategoryToReturnDto>()
            .ForMember(dest => dest.Books, opt => opt.MapFrom(s => s.BookCategories.Select(bc => bc.Book)));
    }
}
