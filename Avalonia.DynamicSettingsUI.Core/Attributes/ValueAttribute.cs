namespace Avalonia.DynamicSettingsUI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ValueAttribute(object? value, object? defaultValue = null) : Attribute
{
    public object? Value { get; set; } = value;
    public object? DefaultValue { get; set; } = defaultValue;
}
