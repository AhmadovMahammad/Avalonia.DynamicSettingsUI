namespace Avalonia.DynamicSettingsUI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class DisplayAttribute(string name, string? description) : Attribute
{
    public string Name { get; set; } = name;
    public string? Description { get; set; } = description;
}
