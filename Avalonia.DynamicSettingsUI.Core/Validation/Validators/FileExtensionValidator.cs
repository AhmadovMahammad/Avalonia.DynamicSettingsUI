using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class FileExtensionValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.FileExtension;

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
                "FileExtension validator requires at least one extension parameter"
            );
        }

        if (context.Value is not string filePath)
        {
            return ValidationResult.Failure(
                propertyName,
                "FileExtension validator requires string input type"
            );
        }

        string fileExtension = Path.GetExtension(filePath);

        List<string?> allowedExtensions = [.. context.Parameters
            .Select(p => p.ToString()?.ToLowerInvariant())
            .Where(e => !string.IsNullOrEmpty(e))];

        if (!allowedExtensions.Contains(fileExtension))
        {
            string extensionList = string.Join(", ", allowedExtensions);

            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must have one of these extensions: {extensionList}"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
