using Renk72Lk.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Models;

public class PasswordResetModel
{
    [Required(ErrorMessage = "Не указана почта.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    [RequiredAlphaDigitPassword(ErrorMessage = "Пароль должен содержать лат. буквы и цифры.")]
    [Required(ErrorMessage = "Не указан пароль.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль должен содержать минимум 8 символов.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
    public string ConfirmPassword { get; set; } = null!;
}
