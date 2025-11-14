using Avalonia.DynamicSettingsUI.Core.Enums;

namespace Avalonia.DynamicSettingsUI.Core.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ControlTypeAttribute(ControlType controlType) : Attribute
{
    public ControlType ControlType { get; set; } = controlType;
}
