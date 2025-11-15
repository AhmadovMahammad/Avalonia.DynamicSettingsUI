using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class DropdownFactory : IControlFactory
{
    public ControlType ControlType => ControlType.Dropdown;

    public Control CreateControl(SettingsMetadata metadata)
    {
        return new ComboBox
        {
            Classes = { "settings-combo" },
            ItemsSource = metadata.Options,
            Width = 400,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
        };
    }
}