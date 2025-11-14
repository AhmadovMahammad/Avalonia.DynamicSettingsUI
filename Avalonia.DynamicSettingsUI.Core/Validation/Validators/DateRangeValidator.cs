using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class DateRangeValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.DateRange;

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
                "DateRange validator requires min and max date parameters"
            );
        }

        if (context.Value is not DateTime dateValue)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a valid date"
            );
        }

        if (!DateTime.TryParse(context.Parameters[0]?.ToString(), out DateTime minDate))
        {
            return ValidationResult.Failure(
                propertyName,
                "Invalid minimum date parameter"
            );
        }

        if (!DateTime.TryParse(context.Parameters[1]?.ToString(), out DateTime maxDate))
        {
            return ValidationResult.Failure(
                propertyName,
                "Invalid maximum date parameter"
            );
        }

        if (dateValue < minDate || dateValue > maxDate)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be between {minDate:d} and {maxDate:d}"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}