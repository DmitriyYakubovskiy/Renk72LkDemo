using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Renk72Lk.Attributes;

public class LatinAndDigitsOnlyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        string stringValue = value.ToString();

        if (string.IsNullOrEmpty(stringValue))
        {
            return ValidationResult.Success;
        }

        Regex latinAndDigits = new Regex("^[a-zA-Z0-9]+$");

        if (!latinAndDigits.IsMatch(stringValue))
        {
            return new ValidationResult(ErrorMessage ?? "Поле должно содержать только латинские буквы и цифры.");
        }

        return ValidationResult.Success;
    }
}
