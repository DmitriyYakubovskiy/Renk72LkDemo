using Renk72Lk.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Renk72Lk.Attributes;

public class MessageViewModelAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var model = (MessageViewModel)validationContext.ObjectInstance;

        if (string.IsNullOrEmpty(model.Text) && (model.DocFiles == null || model.DocFiles.Count == 0))
        {
            return new ValidationResult(ErrorMessage ?? "Необходимо написать сообщение или загрузить файл.");
        }

        return ValidationResult.Success!;
    }
}
