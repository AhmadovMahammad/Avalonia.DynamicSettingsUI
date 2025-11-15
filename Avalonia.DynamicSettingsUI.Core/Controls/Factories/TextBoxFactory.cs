using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class TextBoxFactory : IControlFactory
{
    public ControlType ControlType => ControlType.TextBox;

    public Control CreateControl(SettingsMetadata metadata)
    {
        TextBox textBox = new TextBox
        {
            Classes = { "settings-textbox" },
            Width = 400,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
        };

        if (metadata.Value is string value)
        {
            textBox.Text = value;
        }

        return textBox;
    }
}