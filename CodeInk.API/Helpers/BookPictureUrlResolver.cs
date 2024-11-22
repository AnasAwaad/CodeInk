using AutoMapper;
using CodeInk.API.DTOs;
using CodeInk.Core.Entities;

namespace CodeInk.API.Helpers;

public class BookPictureUrlResolver : IValueResolver<Book, BookToReturnDto, string>
{
    private readonly IConfiguration _configuration;

    public BookPictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(Book source, BookToReturnDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.CoverImageUrl))
            return $"{_configuration["APIBaseUrl"]}/{source.CoverImageUrl}";

        return string.Empty;
    }
}
