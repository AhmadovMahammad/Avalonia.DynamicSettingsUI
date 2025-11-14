using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class PastDateValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.PastDate;

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

        if (context.Value is not DateTime dateValue)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a valid date"
            );
        }

        if (dateValue >= DateTime.Now)
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a past date"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
