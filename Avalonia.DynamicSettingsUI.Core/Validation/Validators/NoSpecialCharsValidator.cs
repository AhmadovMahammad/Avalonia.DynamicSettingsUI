using System.Text.RegularExpressions;

using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class NoSpecialCharsValidator : IValidator
{
    private static readonly Regex NoSpecialCharsRegex = new Regex(@"^[a-zA-Z0-9\s]*$", RegexOptions.Compiled);

    public ValidationType ValidationType => ValidationType.NoSpecialChars;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null or "")
        {
            return ValidationResult.Success(propertyName);
        }

        if (context.Value is not string str)
        {
            return ValidationResult.Failure(
                propertyName,
                "NoSpecialChars validator requires string input type"
            );
        }

        if (!NoSpecialCharsRegex.IsMatch(str))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must not contain special characters"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}
