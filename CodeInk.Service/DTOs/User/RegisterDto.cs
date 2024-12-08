using System.ComponentModel.DataAnnotations;

namespace CodeInk.Service.DTOs.User;
public class RegisterDto
{
    [Required(ErrorMessage = "Display Name is required")]
    public string DisplayName { get; set; }

    [Required(ErrorMessage = "Username is required")]
    //[RegularExpression(@"^[a-zA-Z0-9]([a-zA-Z0-9-_]{3,18}[a-zA-Z0-9])?$", ErrorMessage = "Username must be alphanumeric and can include hyphens and underscores. It should be between 5 and 20 characters.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    //[RegularExpression(
    //    "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,}$",
    //    ErrorMessage = "Password must be at least 6 characters long and include at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character.")]
    public string Password { get; set; }
}
