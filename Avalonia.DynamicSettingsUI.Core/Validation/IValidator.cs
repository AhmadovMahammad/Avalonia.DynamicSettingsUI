using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Validation;

public interface IValidator
{
    ValidationType ValidationType { get; }
    ValidationResult Validate(ValidationContext context);
}