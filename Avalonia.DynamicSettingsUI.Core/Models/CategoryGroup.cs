namespace Avalonia.DynamicSettingsUI.Core.Models;

public class CategoryGroup
{
    public string Name { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public List<SettingsMetadata> Metadatas { get; set; } = [];
    public Core.SettingsBase? SettingsInstance { get; set; }
}
