using CodeInk.Core.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace CodeInk.Repository.Identity;
public static class AppIdentityDbSeed
{
    public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("Customer"));
        }

        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser
            {
                DisplayName = "Admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                PhoneNumber = "01067873327"
            };

            await userManager.CreateAsync(user, "password");

            await userManager.AddToRoleAsync(user, "Admin");
        }


    }
}
