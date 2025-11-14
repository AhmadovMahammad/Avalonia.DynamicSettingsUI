using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class GreaterThanValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.GreaterThan;

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
                "GreaterThan validator requires threshold parameter"
            );
        }

        if (context.Value is not IConvertible convertible)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a numeric value"
            );
        }

        double value = convertible.ToDouble(null);

        double parameterValue = Convert.ToDouble(context.Parameters[0]);

        if (value <= parameterValue)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be greater than {parameterValue}"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
