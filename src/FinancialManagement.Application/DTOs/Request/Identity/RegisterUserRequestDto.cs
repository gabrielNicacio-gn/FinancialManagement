using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialManagement.Application.DTOs.Request.Identity;
public class RegisterUserRequestDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserName is required")]
    [StringLength(50, ErrorMessage = "Username must be between 8 and 50 characters", MinimumLength = 8)]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(50, ErrorMessage = "Password must be between 8 and 50 characters", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[\d])(?=.*[@%#$*^{}&]).+$"
        , ErrorMessage = "Password does no meet requirements")]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessage = "Passwords must be the same")]
    public string PasswordConfirmation { get; set; } = string.Empty;
}
