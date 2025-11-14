using System.Text.RegularExpressions;

using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class EmailValidator : IValidator
{
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public ValidationType ValidationType => ValidationType.Email;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null or "")
        {
            return ValidationResult.Success(propertyName);
        }

        if (context.Value is not string email)
        {
            return ValidationResult.Failure(
                propertyName,
                "Email validator requires string input type"
            );
        }

        if (!EmailRegex.IsMatch(email))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a valid email address"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}