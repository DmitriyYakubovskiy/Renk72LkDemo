using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Renk72Lk.Attributes;

public class RequiredAlphaDigitPasswordAttribute:ValidationAttribute
{
    public RequiredAlphaDigitPasswordAttribute()
    {
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success!;
        }

        var password = value.ToString();

        if (!Regex.IsMatch(password!, @"[A-Za-z]") || !Regex.IsMatch(password!, @"[0-9]"))
        {
            return new ValidationResult(ErrorMessage ?? "Поле не содержит букв или цифр.");
        }

        return ValidationResult.Success!;
    }
}
