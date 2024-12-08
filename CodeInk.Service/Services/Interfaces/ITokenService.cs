using CodeInk.Core.Entities.IdentityEntities;

namespace CodeInk.Service.Services.Interfaces;
public interface ITokenService
{
    string GenerateToken(ApplicationUser appUser, IList<string> roles);
}
