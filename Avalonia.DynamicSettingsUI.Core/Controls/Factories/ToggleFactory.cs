using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class ToggleFactory : IControlFactory
{
    public ControlType ControlType => ControlType.Toggle;

    public Control CreateControl(SettingsMetadata metadata)
    {
        return new ToggleButton { Classes = { "settings-toggle" } };
    }
}