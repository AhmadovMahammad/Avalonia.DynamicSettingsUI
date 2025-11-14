namespace Avalonia.DynamicSettingsUI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CategoryAttribute(string category) : Attribute
{
    public string Category { get; set; } = category;
}
