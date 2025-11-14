namespace Avalonia.DynamicSettingsUI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class OptionsAttribute(params string[] options) : Attribute
{
    public string[] Options { get; set; } = options;
}
