namespace Avalonia.DynamicSettingsUI.Core.Models;

public class ValidationContext
{
    public string PropertyName { get; set; } = string.Empty;
    public object[] Parameters { get; set; } = [];
    public object? Value { get; set; }
}
