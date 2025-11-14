using System.Text.RegularExpressions;

using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class AlphaNumericValidator : IValidator
{
    private static readonly Regex AlphaNumericRegex = new Regex(@"^[a-zA-Z0-9\s,]*$", RegexOptions.Compiled);

    public ValidationType ValidationType => ValidationType.AlphaNumeric;

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

        if (context.Value is string str)
        {
            return AlphaNumericRegex.IsMatch(str)
                ? ValidationResult.Success(context.PropertyName)
                : ValidationResult.Failure(
                    context.PropertyName,
                    $"{context.PropertyName} cannot be empty"
                );
        }

        return ValidationResult.Failure(
               propertyName,
               "AlphaNumeric validator requires string input type"
        );
    }
}