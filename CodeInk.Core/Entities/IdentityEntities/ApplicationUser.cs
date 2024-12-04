using Microsoft.AspNetCore.Identity;

namespace CodeInk.Core.Entities.IdentityEntities;
public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; }
    public Address Address { get; set; }
}
