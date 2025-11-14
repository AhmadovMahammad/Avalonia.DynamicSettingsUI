namespace Avalonia.DynamicSettingsUI.Core.Models;

public class ValidationResult
{
    public bool IsValid { get; init; }
    public string PropertyName { get; init; } = string.Empty;
    public string? ErrorMessage { get; init; }

    public static ValidationResult Success(string propertyName)
    {
        return new ValidationResult()
        {
            IsValid = true,
            PropertyName = propertyName
        };
    }

    public static ValidationResult Failure(string propertyName, string errorMessage)
    {
        return new ValidationResult()
        {
            IsValid = false,
            PropertyName = propertyName,
            ErrorMessage = errorMessage,
        };
    }
}