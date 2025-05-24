using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Attributes;

public class MustBeTrueAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value==null) return ValidationResult.Success!;

        if (value is bool boolValue && boolValue)
        {
            return ValidationResult.Success!;
        }

 
        if (value is string && (value.ToString()=="on" || value.ToString() == "true"))
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult(ErrorMessage ?? "Обязательное поле.");
    }
}