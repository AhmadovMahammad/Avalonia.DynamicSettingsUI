using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.DynamicSettingsUI.Core.Binding;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;

namespace Avalonia.DynamicSettingsUI.Core.Controls;

internal sealed class SettingsControlBuilder(
    ControlFactoryRegistry controlFactoryRegistry,
    BindingStrategyRegistry bindingStrategyRegistry)
{
    private Panel? _contentPanel;
    private readonly Dictionary<string, Control> _categoryPanels = [];

    public Control BuildControl(IEnumerable<SettingsGroupLayer> groupLayers)
    {
        Grid mainGrid = new Grid
        {
            RowDefinitions =
            [
                new RowDefinition(1, GridUnitType.Star),
                new RowDefinition(1, GridUnitType.Auto)
            ]
        };

        CreateBody(mainGrid, groupLayers);

        CreateFooter(mainGrid, groupLayers);

        return mainGrid;
    }

    private void CreateBody(Grid mainGrid, IEnumerable<SettingsGroupLayer> groupLayers)
    {
        Grid bodyGrid = new Grid
        {
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = new GridLength(300), MinWidth = 250, MaxWidth = 350 },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
            ]
        };

        // column 0: tree view with categories
        Control treeView = CreateTreeView(groupLayers);

        Grid.SetColumn(treeView, 0);

        bodyGrid.Children.Add(treeView);

        // column 1: splitter
        GridSplitter splitter = new GridSplitter
        {
            Classes = { "settings-splitter" },
            ShowsPreview = true,
            ResizeBehavior = GridResizeBehavior.BasedOnAlignment,
            ResizeDirection = GridResizeDirection.Columns
        };

        Grid.SetColumn(splitter, 1);

        bodyGrid.Children.Add(splitter);

        // column 2: content panel
        _contentPanel = new Panel();

        Grid.SetColumn(_contentPanel, 2);

        bodyGrid.Children.Add(_contentPanel);

        mainGrid.Children.Add(bodyGrid);
    }

    private Control CreateTreeView(IEnumerable<SettingsGroupLayer> groupLayers)
    {
        foreach (SettingsGroupLayer group in groupLayers)
        {
            foreach (CategoryGroup category in group.Categories)
            {
                // create content panel here and add into dictionary
                string key = $"{group.Name}_{category.Name}";
            }
        }

        TreeView treeView = new TreeView
        {
            Background = Brushes.Transparent,
            BorderThickness = new Thickness(0),
            ItemsSource = groupLayers,
            ItemTemplate = new FuncTreeDataTemplate<object>(
                (element, _) =>
                {
                    if (element is SettingsGroupLayer group)
                    {
                        return new TextBlock { Text = group.Name };
                    }

                    if (element is CategoryGroup category)
                    {
                        return new TextBlock { Text = category.Name };
                    }

                    return new TextBlock();
                },
                element =>
                {
                    if (element is SettingsGroupLayer group)
                    {
                        return group.Categories;
                    }

                    return new List<object>();
                }
            )
        };

        SettingsGroupLayer? firstGroup = groupLayers.FirstOrDefault();

        if (firstGroup?.Categories.Count > 0)
        {
            treeView.SelectedItem = firstGroup.Categories[0];
        }

        return treeView;
    }

    private void CreateFooter(Grid mainGrid, IEnumerable<SettingsGroupLayer> groupLayers)
    {
        Border footerBorder = new Border
        {
            Padding = new Thickness(10, 8),
            BorderThickness = new Thickness(0, 1, 0, 0),
            BorderBrush = new SolidColorBrush(Colors.Gray) { Opacity = 0.3 }
        };

        StackPanel footerPanel = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            Spacing = 8
        };

        Button okButton = new Button
        {
            Content = "OK",
            Classes = { "settings-btn-base" },
            MinWidth = 80
        };

        Button cancelButton = new Button
        {
            Content = "Cancel",
            Classes = { "settings-btn-base" },
            MinWidth = 80
        };

        okButton.Click += (s, e) =>
        {
            foreach (var group in groupLayers.SelectMany(gp => gp.Categories))
            {
                group.SettingsInstance?.Save(null);
            }
        };

        footerPanel.Children.Add(okButton);

        footerPanel.Children.Add(cancelButton);

        footerBorder.Child = footerPanel;

        Grid.SetRow(footerBorder, 1);

        mainGrid.Children.Add(footerBorder);
    }
}