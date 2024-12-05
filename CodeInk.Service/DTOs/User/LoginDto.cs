using System.ComponentModel.DataAnnotations;

namespace CodeInk.Service.DTOs.User;
public class LoginDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
