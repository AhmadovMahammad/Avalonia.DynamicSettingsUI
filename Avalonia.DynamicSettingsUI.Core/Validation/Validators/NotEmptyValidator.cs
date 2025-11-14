using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class NotEmptyValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.NotEmpty;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} cannot be empty"
            );
        }

        bool isValid = context.Value switch
        {
            string str => !string.IsNullOrEmpty(str),
            System.Collections.ICollection collection => collection.Count > 0,
            _ => true
        };

        return isValid
            ? ValidationResult.Success(propertyName)
            : ValidationResult.Failure(
                propertyName,
                $"{propertyName} cannot be empty"
            );
    }
}
