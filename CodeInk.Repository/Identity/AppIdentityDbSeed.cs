using CodeInk.Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace CodeInk.Repository.Identity;
public static class AppIdentityDbSeed
{
    public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
    {
        var user = new ApplicationUser
        {
            DisplayName = "Anas Awaad",
            UserName = "Anas_3waad",
            Email = "anas.shaban.awaad@gmail.com",
            PhoneNumber = "01067873327"
        };
        var userExist = await userManager.FindByEmailAsync(user.Email);
        if (userExist is null)
        {
            await userManager.CreateAsync(user, "Pa$$w0rd");
        }
    }
}
