using System.Reflection;

using Avalonia.DynamicSettingsUI.Core.Enums;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.DynamicSettingsUI.Core.Models;

public class SettingsMetadata
{
    public PropertyInfo PropertyInfo { get; set; } = null!;
    public string Category { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ControlType ControlType { get; set; } = ControlType.TextBox;
    public object? Value { get; set; }
    public object? DefaultValue { get; set; }
    public string[]? Options { get; set; }
    public string GroupName { get; set; } = string.Empty;
}