using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.DynamicSettingsUI.Core.Binding;
using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;

namespace Avalonia.DynamicSettingsUI.Core.Controls;

internal sealed class SettingsControlBuilder(
    ControlFactoryRegistry controlFactoryRegistry,
    BindingStrategyRegistry bindingStrategyRegistry)
{
    private readonly Panel _contentPanel = new Panel();
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
                Control control = CreateCategoryPanel(category);

                control.IsVisible = false;

                string key = $"{group.Name}_{category.Name}";

                _categoryPanels[key] = control;
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
                    return element switch
                    {
                        SettingsGroupLayer group => new TextBlock { Text = group.Name },
                        CategoryGroup category => new TextBlock { Text = category.Name },
                        _ => new TextBlock { }
                    };
                },
                element =>
                {
                    return element switch
                    {
                        SettingsGroupLayer group => group.Categories,
                        _ => []
                    };
                }
            )
        };

        treeView.SelectionChanged += (s, e) =>
        {
            if (s is TreeView treeView1 && treeView1.SelectedItem is CategoryGroup category)
            {
                ShowCategoryPanel(category);
            }
        };

        if (groupLayers.FirstOrDefault() is { Categories.Count: > 0 } firstGroup)
        {
            treeView.SelectedItem = firstGroup.Categories[0];
        }

        return treeView;
    }

    private void ShowCategoryPanel(CategoryGroup category)
    {
        if (_contentPanel == null)
        {
            return;
        }

        string key = $"{category.GroupName}_{category.Name}";

        foreach (var panel in _categoryPanels)
        {
            panel.Value.IsVisible = panel.Key.Equals(key, StringComparison.OrdinalIgnoreCase);
        }

        _contentPanel.Children.Clear();

        if (_categoryPanels.TryGetValue(key, out Control? control) && control != null)
        {
            _contentPanel.Children.Add(control);
        }
    }

    private Control CreateCategoryPanel(CategoryGroup category)
    {
        ScrollViewer scrollViewer = new ScrollViewer
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Padding = new Thickness(20)
        };

        StackPanel stackPanel = new StackPanel();

        Border headerBorder = new Border
        {
            BorderThickness = new Thickness(0, 0, 0, 1),
            BorderBrush = new SolidColorBrush(Color.Parse("#00437f")),
            Margin = new Thickness(0, 0, 0, 20),
            Child = new TextBlock
            {
                Text = category.Name,
                FontSize = 20,
                FontWeight = FontWeight.Bold,
                Margin = new Thickness(0, 0, 0, 8)
            }
        };

        stackPanel.Children.Add(headerBorder);

        foreach (SettingsMetadata metadata in category.Metadatas)
        {
            stackPanel.Children.Add(CreateSettingBlock(metadata, category.SettingsInstance));
        }

        scrollViewer.Content = stackPanel;

        scrollViewer.IsVisible = false;

        return scrollViewer;
    }

    private Control CreateSettingBlock(SettingsMetadata metadata, SettingsBase? settingsInstance)
    {
        Border border = new Border { Classes = { "settings-block" } };

        StackPanel stackPanel = new StackPanel();

        stackPanel.Children.Add(new TextBlock
        {
            Text = metadata.Name,
            FontWeight = FontWeight.SemiBold,
            Margin = new Thickness(0, 0, 0, 2)
        });

        // update grid, if boolean, description on right side of control

        if (!string.IsNullOrWhiteSpace(metadata.Description))
        {
            stackPanel.Children.Add(new TextBlock
            {
                Text = metadata.Description,
                FontSize = 11,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 6),
                Opacity = 0.7
            });
        }

        if (controlFactoryRegistry.GetFactory(metadata.ControlType) is { } factory)
        {
            Control? inputControl = factory.CreateControl(metadata);

            inputControl.DataContext = settingsInstance;

            if (bindingStrategyRegistry.GetStrategy(metadata.ControlType) is { } bindingStrategy)
            {
                Data.Binding binding = new Data.Binding(metadata.PropertyInfo.Name, BindingMode.TwoWay);
                bindingStrategy.ApplyBinding(binding, inputControl, metadata);
            }

            stackPanel.Children.Add(inputControl);
        }

        border.Child = stackPanel;

        return border;
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