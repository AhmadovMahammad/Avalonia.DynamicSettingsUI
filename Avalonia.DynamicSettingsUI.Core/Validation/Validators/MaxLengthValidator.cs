using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class MaxLengthValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.MaxLength;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null or "")
        {
            return ValidationResult.Success(propertyName);
        }

        if (context.Parameters.Length < 1)
        {
            return ValidationResult.Failure(
                propertyName,
                "MaxLength validator requires length parameter"
            );
        }

        if (!int.TryParse(context.Parameters[0]?.ToString(), out int maxLength))
        {
            return ValidationResult.Failure(
                propertyName,
                "Invalid length parameter"
            );
        }

        if (context.Value is not string str)
        {
            return ValidationResult.Failure(
                propertyName,
                "MaxLength validator requires string input type"
            );
        }

        if (str.Length > maxLength)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must not exceed {maxLength} characters"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
