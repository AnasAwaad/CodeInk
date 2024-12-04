using CodeInk.Core.Entities.IdentityEntities;
using CodeInk.Repository.Identity;
using Microsoft.AspNetCore.Identity;

namespace CodeInk.API.Extensions;

public static class IdentityServicesExtension
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection Services)
    {
        Services.AddIdentity<ApplicationUser, IdentityRole>()   // add iterfaces
                        .AddEntityFrameworkStores<AppIdentityDbContext>();  // add classes that implement interfaces

        Services.AddAuthentication();   // UserManager , SignInManager , RoleManager

        return Services;
    }
}
