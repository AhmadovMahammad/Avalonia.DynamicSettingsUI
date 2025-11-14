using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class RangeValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.Range;

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

        if (context.Parameters.Length < 2)
        {
            return ValidationResult.Failure(
                propertyName,
                "Range validator requires 2 parameters to validate input."
            );
        }

        double min = Convert.ToDouble(context.Parameters[0]);

        double max = Convert.ToDouble(context.Parameters[1]);

        if (context.Value is not IConvertible convertible)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a numeric value");
        }

        double numericValue = convertible.ToDouble(null);

        if (numericValue < min || numericValue > max)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be between {min} and {max}"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
