using CodeInk.API.Errors;
using CodeInk.Application.Mapping;
using CodeInk.Application.Services.Implementations;
using CodeInk.Application.Services.Interfaces;
using CodeInk.Core.Repositories;
using CodeInk.Core.Service;
using CodeInk.Repository;
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

        Services.AddAutoMapper(typeof(MappingProfiles));

        Services.Configure<ApiBehaviorOptions>(options =>
        {

            // override this default behavior
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(P => P.Value?.Errors.Count > 0)
                                                     .SelectMany(P => P.Value!.Errors.Select(e => e.ErrorMessage))
                                                     .ToList();

                var apiValidationError = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(apiValidationError);
            };

        });

        return Services;
    }
}
