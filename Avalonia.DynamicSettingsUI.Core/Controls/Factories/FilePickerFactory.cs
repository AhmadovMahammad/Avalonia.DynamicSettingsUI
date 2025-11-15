using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;
using Avalonia.Platform.Storage;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class FilePickerFactory(bool allowMultipleFiles) : IControlFactory
{
    public ControlType ControlType => allowMultipleFiles ? ControlType.MultiFilePicker : ControlType.FilePicker;

    public Control CreateControl(SettingsMetadata metadata)
    {
        Grid mainGrid = new Grid()
        {
            Width = 400,
            HorizontalAlignment = HorizontalAlignment.Left,
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = new GridLength(7, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) }
            ]
        };

        Button button = new Button
        {
            Classes = { "settings-btn-base" },
            Content = "Browse...",
            Margin = new Thickness(6, 0, 0, 0)
        };

        Grid.SetColumn(button, 1);

        mainGrid.Children.Add(button);

        TextBox textBox = new TextBox
        {
            HorizontalAlignment = HorizontalAlignment.Stretch,
            IsReadOnly = true,
            AcceptsReturn = true,
        };

        button.Click += async (s, e) =>
        {
            if (TopLevel.GetTopLevel(button) is { } topLevel)
            {
                IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                {
                    AllowMultiple = allowMultipleFiles,
                    Title = "Select File" + (allowMultipleFiles ? "" : "s")
                });

                if (files.Count > 0)
                {
                    string pathToShow = string.Join('\n', files.Select(t => t.Path.LocalPath));
                    textBox.Text = pathToShow;
                }
            }
        };

        Grid.SetColumn(textBox, 0);

        mainGrid.Children.Add(textBox);

        return mainGrid;
    }
}
