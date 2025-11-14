using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class ExactLengthValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.ExactLength;

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
                "ExactLength validator requires length parameter"
            );
        }

        if (!int.TryParse(context.Parameters[0]?.ToString(), out int exactLength))
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
                "ExactLength validator requires string input type"
            );
        }

        if (str.Length != exactLength)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be exactly {exactLength} characters"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
