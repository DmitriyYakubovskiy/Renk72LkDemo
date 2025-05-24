using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Attributes;

public class NotEmptyMaxStringLengthAttribute : ValidationAttribute
{
    private readonly int maxLength;

    public NotEmptyMaxStringLengthAttribute(int maxLength)
    {
        this.maxLength = maxLength;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success!;
        }

        if (value.ToString()!.Length > maxLength)
        {
            return new ValidationResult(ErrorMessage ?? $"Длина поля не должна превышать {maxLength} символов.");
        }

        return ValidationResult.Success!;
    }
}
