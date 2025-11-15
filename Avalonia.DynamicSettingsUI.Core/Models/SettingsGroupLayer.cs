using System.Collections.ObjectModel;

namespace Avalonia.DynamicSettingsUI.Core.Models;

public class SettingsGroupLayer
{
    public string Name { get; set; } = string.Empty;
    public ObservableCollection<CategoryGroup> Categories { get; set; } = [];
}
