using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class NotWhiteSpaceValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.NotWhiteSpace;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} is required"
            );
        }

        if (context.Value is not string str)
        {
            return ValidationResult.Failure(
                propertyName,
                "NotWhiteSpace validator requires string input type"
            );
        }

        if (string.IsNullOrWhiteSpace(str))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} cannot be empty or whitespace"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
