using System.Text.RegularExpressions;

using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class AlphaOnlyValidator : IValidator
{
    private static readonly Regex AlphaRegex = new Regex(@"^[a-zA-Z\s]*$", RegexOptions.Compiled);

    public ValidationType ValidationType => ValidationType.AlphaOnly;

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

        if (context.Value is not string str)
        {
            return ValidationResult.Failure(
                propertyName,
                "AlphaOnly validator requires string input type"
            );
        }

        return AlphaRegex.IsMatch(str)
            ? ValidationResult.Success(propertyName)
            : ValidationResult.Failure(
                propertyName,
                $"{propertyName} must contain only letters"
            );
    }
}