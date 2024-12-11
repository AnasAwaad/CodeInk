using CodeInk.API.Factories;
using CodeInk.Application.Mapping;
using CodeInk.Application.Services.Implementations;
using CodeInk.Application.Services.Interfaces;
using CodeInk.Core.Repositories;
using CodeInk.Core.Service;
using CodeInk.Repository;
using CodeInk.Repository.Basket;
using CodeInk.Service.Services.Implementations;
using CodeInk.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeInk.API.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
    {
        Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        Services.AddScoped<IFileService, FileService>();
        Services.AddScoped<IBookService, BookService>();
        Services.AddScoped<ICategoryService, CategoryService>();
        Services.AddScoped<IBasketRepository, BasketRepository>();
        Services.AddScoped<ICacheService, CacheService>();
        Services.AddScoped<IBasketService, BasketService>();
        Services.AddScoped<ITokenService, TokenService>();
        Services.AddScoped<IUserService, UserService>();
        Services.AddScoped<IOrderService, OrderService>();
        Services.AddScoped<IPaymentService, PaymentService>();

        Services.AddAutoMapper(typeof(MappingProfiles));

        // override this default behavior of validation error response
        Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory =
                actionContext => ApiResponseFactory.CustomeValidationErrorResponse(actionContext);
        });

        return Services;
    }
}
