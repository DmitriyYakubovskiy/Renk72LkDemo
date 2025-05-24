using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Attributes;

public class RequiredIfBothAttribute : ValidationAttribute
{
    private readonly string firstComparisonProperty;
    private readonly string[] firstComparisonValues;
    private readonly string secondComparisonProperty;
    private readonly string[] secondComparisonValues;

    public RequiredIfBothAttribute(string firstComparisonProperty, string[] firstComparisonValues,
                                   string secondComparisonProperty, string[] secondComparisonValues)
    {
        this.firstComparisonProperty = firstComparisonProperty;
        this.firstComparisonValues = firstComparisonValues;
        this.secondComparisonProperty = secondComparisonProperty;
        this.secondComparisonValues = secondComparisonValues;

    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var propertyInfo1 = validationContext.ObjectType.GetProperty(firstComparisonProperty);

        if (propertyInfo1 == null)
        {
            throw new ArgumentException("First property not found", nameof(firstComparisonProperty));
        }

        var comparisonValue1 = propertyInfo1.GetValue(validationContext.ObjectInstance)?.ToString();

        var propertyInfo2 = validationContext.ObjectType.GetProperty(secondComparisonProperty);
        if (propertyInfo2 == null)
        {
            throw new ArgumentException("Second property not found", nameof(secondComparisonProperty));
        }

        var comparisonValue2 = propertyInfo2.GetValue(validationContext.ObjectInstance)?.ToString();

        if ((firstComparisonValues.Contains(comparisonValue1) && secondComparisonValues.Contains(comparisonValue1)) && (string.IsNullOrWhiteSpace(value?.ToString())))
        {
            return new ValidationResult(ErrorMessage ?? $"Обязательное поле.");
        }
        return ValidationResult.Success!;
    }
}
