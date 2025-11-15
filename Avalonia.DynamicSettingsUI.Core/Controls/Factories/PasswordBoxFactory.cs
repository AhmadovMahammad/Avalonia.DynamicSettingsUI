using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class PasswordBoxFactory : IControlFactory
{
    public ControlType ControlType => ControlType.PasswordBox;

    public Control CreateControl(SettingsMetadata metadata)
    {
        return new TextBox
        {
            Classes = { "settings-textbox" },
            Width = 400,
            PasswordChar = '*',
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
        };
    }
}
