using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class NumericFactory : IControlFactory
{
    public ControlType ControlType => ControlType.Numeric;

    public Control CreateControl(SettingsMetadata metadata)
    {
        return new NumericUpDown
        {
            Classes = { "settings-numeric" },
            Width = 400,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
        };
    }
}