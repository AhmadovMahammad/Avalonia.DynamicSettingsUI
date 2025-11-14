using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation.Validators;

public class UrlValidator : IValidator
{
    public ValidationType ValidationType => ValidationType.Url;

    public ValidationResult Validate(ValidationContext context)
    {
        string propertyName = context.PropertyName;

        if (context.Value is null or "")
        {
            return ValidationResult.Success(propertyName);
        }

        if (context.Value is not string url)
        {
            return ValidationResult.Failure(
                propertyName,
                "Url validator requires string input type"
            );
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) || (uriResult.Scheme != Uri.UriSchemeHttp && uriResult.Scheme != Uri.UriSchemeHttps))
        {
            return ValidationResult.Failure(
                propertyName,
                $"{propertyName} must be a valid URL"
            );
        }

        return ValidationResult.Success(propertyName);
    }
}