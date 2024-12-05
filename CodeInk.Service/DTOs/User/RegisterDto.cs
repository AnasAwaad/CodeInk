using System.ComponentModel.DataAnnotations;

namespace CodeInk.Service.DTOs.User;
public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [RegularExpression(
        "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[^a-zA-Z\\d]).{6,10}$",
        ErrorMessage = "Password must be 6-10 characters long and include at least 1 uppercase letter, 1 lowercase letter, 1 number, and 1 special character.")]
    public string Password { get; set; }
}
