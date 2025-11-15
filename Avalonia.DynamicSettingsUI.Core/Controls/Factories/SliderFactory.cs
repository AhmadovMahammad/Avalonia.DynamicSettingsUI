using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class SliderFactory : IControlFactory
{
    public ControlType ControlType => ControlType.Slider;

    public Control CreateControl(SettingsMetadata metadata)
    {
        StackPanel panel = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10 };

        Slider slider = new Slider
        {
            Name = "settings-slider",
            Width = 380,
            TickFrequency = 1,
            IsSnapToTickEnabled = true,
        };

        TextBlock valueText = new TextBlock
        {
            Width = 20,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 12
        };

        slider.PropertyChanged += (s, e) =>
        {
            if (e.Property.Name == nameof(Slider.Value))
            {
                valueText.Text = ((int)slider.Value).ToString();
            }
        };

        panel.Children.Add(slider);

        panel.Children.Add(valueText);

        return panel;
    }
}