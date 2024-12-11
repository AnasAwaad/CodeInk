using AutoMapper;
using CodeInk.Application.DTOs.Book;
using CodeInk.Application.DTOs.Category;
using CodeInk.Application.Mapping.Resolvers;
using CodeInk.Core.Entities;
using CodeInk.Core.Entities.OrderEntities;
using CodeInk.Repository.Models;
using CodeInk.Service.DTOs.Basket;
using CodeInk.Service.DTOs.Order;
using CodeInk.Service.DTOs.Payment;
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

        // order mapping
        CreateMap<ShippingAddressDto, Address>();
        CreateMap<DeliveryMethod, DeliveryMethodDto>();

        CreateMap<OrderItem, CartItemDto>();
        CreateMap<OrderRequestDto, PaymentCartDto>()
            .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.OrderItems));

        CreateMap<Order, OrderResultDto>()
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
            .ForMember(dest => dest.DeliveryMethodCost, opt => opt.MapFrom(src => src.DeliveryMethod.Price))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal));
    }
}
