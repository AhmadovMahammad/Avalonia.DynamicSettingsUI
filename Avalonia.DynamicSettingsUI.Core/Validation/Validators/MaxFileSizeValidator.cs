using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class MaxFileSizeValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.MaxFileSize;

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
                "MaxFileSize validator requires size parameter in bytes"
            );
        }

        if (context.Value is not string filePath)
        {
            return ValidationResult.Failure(
                propertyName,
                "MaxFileSize validator requires string input type"
            );
        }

        if (!File.Exists(filePath))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} file does not exist: {filePath}"
            );
        }

        FileInfo fileInfo = new FileInfo(filePath);

        long maxSize = Convert.ToInt64(context.Parameters[0]);

        if (fileInfo.Length > maxSize)
        {
            // 1 KB = 1024 bytes (2^10)
            // 1 MB = 1024 KB => 1024 * 1024 bytes.

            double maxSizeMB = maxSize / (1024.0 * 1024.0);

            double actualSizeMB = fileInfo.Length / (1024.0 * 1024.0);

            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} file size ({actualSizeMB:F2} MB) exceeds maximum allowed size ({maxSizeMB:F2} MB)"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
