using AutoMapper;
using CodeInk.Application.DTOs.Book;
using CodeInk.Application.DTOs.Category;
using CodeInk.Application.Mapping.Resolvers;
using CodeInk.Core.Entities;
using CodeInk.Core.Entities.IdentityEntities;
using CodeInk.Repository.Models;
using CodeInk.Service.DTOs.Basket;
using CodeInk.Service.DTOs.User;

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
            .ForMember(dest => dest.NumOfBooks, opt => opt.MapFrom(s => s.BookCategories.Count()));

        CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();

        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
