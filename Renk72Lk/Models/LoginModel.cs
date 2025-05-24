using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Не указано имя пользователя.")]
    public string Login { get; set; }= null!;

    [Required(ErrorMessage = "Не указан пароль.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public string? ReturnUrl { get; set; } = "/";
}