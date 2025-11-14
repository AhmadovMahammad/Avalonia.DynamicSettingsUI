using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class MinLengthValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.MinLength;

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

        if (context.Parameters.Length < 1)
        {
            return ValidationResult.Failure(
                propertyName,
                "MinLength validator requires length parameter"
            );
        }

        if (!int.TryParse(context.Parameters[0]?.ToString(), out int minLength))
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
                "MinLength validator requires string input type"
            );
        }

        if (str.Length < minLength)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be at least {minLength} characters"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
