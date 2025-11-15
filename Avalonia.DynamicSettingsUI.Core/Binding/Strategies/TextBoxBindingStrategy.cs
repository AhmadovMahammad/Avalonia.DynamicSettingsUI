using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Binding.Strategies;

public class TextBoxBindingStrategy : IBindingStrategy
{
    public ControlType ControlType => ControlType.TextBox;

    public void ApplyBinding(Data.Binding binding, Control control, SettingsMetadata metadata)
    {
        if (control is TextBox textBox)
        {
            textBox.Bind(TextBox.TextProperty, binding);
        }
    }
}