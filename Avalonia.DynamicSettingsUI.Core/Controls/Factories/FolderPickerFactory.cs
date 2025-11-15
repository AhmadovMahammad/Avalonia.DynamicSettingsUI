using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;
using Avalonia.Platform.Storage;

namespace Avalonia.DynamicSettingsUI.Core.Controls.Factories;

public class FolderPickerFactory : IControlFactory
{
    public ControlType ControlType => ControlType.FolderPicker;

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
        };

        button.Click += async (s, e) =>
        {
            if (TopLevel.GetTopLevel(button) is { } topLevel)
            {
                IReadOnlyList<IStorageFolder> folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions { Title = "Select Folder", AllowMultiple = false });

                if (folders.Count > 0)
                {
                    textBox.Text = folders[0].Path.LocalPath;
                }
            }
        };

        Grid.SetColumn(textBox, 0);

        mainGrid.Children.Add(textBox);

        return mainGrid;
    }
}