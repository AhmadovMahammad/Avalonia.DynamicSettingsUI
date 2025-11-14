using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class DirectoryExistsValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.DirectoryExists;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null or "")
        {
            return ValidationResult.Success(propertyName);
        }

        if (context.Value is string path && !Directory.Exists(path))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} directory does not exist: {path}"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}