using Renk72Lk.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Models;

public class RegistrationModel
{
    [LatinAndDigitsOnly(ErrorMessage = "Поле должно содержать только латинские буквы и цифры.")]
    [Required(ErrorMessage = "Не указано имя пользователя.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Логин должен содержать минимум 4 символа.")]
    public string Login { get; set; } = null!;

    [RequiredAlphaDigitPassword(ErrorMessage = "Пароль должен содержать лат. буквы и цифры.")]
    [Required(ErrorMessage = "Не указан пароль.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль должен содержать минимум 8 символов.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
    public string? ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Не указана фамилия.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать минимум 2 символа.")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "Не указано имя.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Имя должно содержать минимум 2 символа.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Не указано отчество.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Отчество должно содержать минимум 2 символа.")]
    public string Patronymic { get; set; } = null!;

    [Required(ErrorMessage = "Не указана почта.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Не указан номер телефона.")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Не указан СНИЛС.")]
    public string? Snils { get; set; } = null!;

    [Required(ErrorMessage = "Файл не выбран.")]
    public IFormFile? UserDataAgreementFormFile { get; set; }
}
