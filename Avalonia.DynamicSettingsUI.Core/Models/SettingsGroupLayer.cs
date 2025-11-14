namespace Avalonia.DynamicSettingsUI.Core.Models;

public class SettingsGroupLayer
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<SettingsMetadata> Settings { get; set; } = [];
}
