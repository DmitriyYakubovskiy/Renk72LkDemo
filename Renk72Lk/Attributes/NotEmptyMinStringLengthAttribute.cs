using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Attributes;

public class NotEmptyMinStringLengthAttribute : ValidationAttribute
{
    private readonly int minLength;

    public NotEmptyMinStringLengthAttribute(int minLength)
    {
        this.minLength = minLength;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
        {
            return ValidationResult.Success!;
        }

        if (value.ToString()!.Length < minLength)
        {
            return new ValidationResult(ErrorMessage ?? $"Длина поля должна превышать {minLength} символов.");
        }

        return ValidationResult.Success!;
    }
}
