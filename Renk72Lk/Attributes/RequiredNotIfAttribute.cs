using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Attributes;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class RequiredNotIfAttribute : ValidationAttribute
{
    private readonly string comparisonProperty;
    private readonly string[] comparisonValues;

    public RequiredNotIfAttribute(string comparisonProperty, string[] comparisonValues)
    {
        this.comparisonProperty = comparisonProperty;
        this.comparisonValues = comparisonValues;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var propertyInfo = validationContext.ObjectType.GetProperty(comparisonProperty);
        if (propertyInfo == null)
        {
            throw new ArgumentException("Property not found", comparisonProperty);
        }

        var comparisonValue = propertyInfo.GetValue(validationContext.ObjectInstance)?.ToString();

        if (!comparisonValues.Contains(comparisonValue) && string.IsNullOrWhiteSpace(value?.ToString()))
        {
            return new ValidationResult(ErrorMessage ?? $"Обязательное поле.");
        }

        return ValidationResult.Success!;
    }
}
