using System.Text.RegularExpressions;

using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class RegexValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.Regex;

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
                "Regex validator requires pattern parameter"
            );
        }

        string? pattern = context.Parameters[0]?.ToString();

        if (string.IsNullOrEmpty(pattern))
        {
            return ValidationResult.Failure(
                propertyName,
                "Regex pattern cannot be empty"
            );
        }

        if (context.Value is not string str)
        {
            return ValidationResult.Failure(
                propertyName,
                "Regex validator requires string input type"
            );
        }

        try
        {
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(str))
            {
                return ValidationResult.Failure(
                    propertyName,
                    $"{propertyName} does not match the required pattern"
                );
            }

            return ValidationResult.Success(propertyName);
        }
        catch (ArgumentException)
        {
            return ValidationResult.Failure(
                propertyName,
                "Invalid regex pattern"
            );
        }
    }
}
