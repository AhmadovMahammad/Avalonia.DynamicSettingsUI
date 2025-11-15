using Avalonia.DynamicSettingsUI.Core.Core;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.DynamicSettingsUI.Core.Models;

public class SettingsGroupLayer
{
    public string Name { get; set; } = string.Empty;
    public IEnumerable<SettingsMetadata> Metadatas { get; set; } = [];
    public SettingsBase SettingsInstance { get; set; } = null!;
}
