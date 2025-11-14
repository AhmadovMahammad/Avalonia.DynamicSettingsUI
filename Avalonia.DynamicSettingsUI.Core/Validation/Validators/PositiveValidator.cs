using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class PositiveValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.Positive;

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

        if (context.Value is not IConvertible convertible)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a numeric value"
            );
        }

        if (convertible.ToDouble(null) <= 0)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a positive number"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
