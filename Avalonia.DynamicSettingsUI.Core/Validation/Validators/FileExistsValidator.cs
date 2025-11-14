using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class FileExistsValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.FileExists;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null or "")
        {
            return ValidationResult.Success(propertyName);
        }

        if (context.Value is not string filePath)
        {
            return ValidationResult.Failure(
                propertyName,
                "FileExists validator requires string input type"
            );
        }

        if (!File.Exists(filePath))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} file does not exist: {filePath}"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
