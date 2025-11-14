using Avalonia.DynamicSettingsUI.Core.Enums;

namespace Avalonia.DynamicSettingsUI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class ValidationAttribute(ValidationType validationType, object[]? parameters) : Attribute
{
    public ValidationType ValidationType { get; set; } = validationType;
    public object[]? Parameters { get; set; } = parameters;
}
