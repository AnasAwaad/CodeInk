using AutoMapper;
using CodeInk.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace CodeInk.Application.Mapping.Resolvers;

public class BookPictureUrlResolver : IValueResolver<Book, object, string>
{
    private readonly IConfiguration _configuration;

    public BookPictureUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(Book source, object destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.CoverImageUrl))
            return $"{_configuration["APIBaseUrl"]}/{source.CoverImageUrl}";

        return string.Empty;
    }
}
