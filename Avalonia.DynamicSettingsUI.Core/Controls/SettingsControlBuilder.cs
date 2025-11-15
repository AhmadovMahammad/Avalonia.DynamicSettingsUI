

using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Binding;
using Avalonia.DynamicSettingsUI.Core.Models;
using Avalonia.Layout;
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
            foreach (var group in groupLayers)
            {
                group.SettingsInstance.Save(null);
            }
        };

        footerPanel.Children.Add(okButton);
        
        footerPanel.Children.Add(cancelButton);

        footerBorder.Child = footerPanel;
        
        Grid.SetRow(footerBorder, 1);
        
        mainGrid.Children.Add(footerBorder);
    }

    //private void CreateBody(Grid mainGrid, IEnumerable<SettingsGroupLayer> groupLayers)
    //{
    //    Grid bodyGrid = new Grid
    //    {
    //        ColumnDefinitions =
    //        [
    //            new ColumnDefinition { Width = new GridLength(300), MinWidth = 250, MaxWidth = 350 },
    //            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
    //            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
    //        ]
    //    };

    //    // column: 0

    //    // column: 1
    //    GridSplitter splitter = new GridSplitter
    //    {
    //        Classes = { "settings-splitter" },
    //        ShowsPreview = true,
    //        ResizeBehavior = GridResizeBehavior.BasedOnAlignment,
    //        ResizeDirection = GridResizeDirection.Columns
    //    };

    //    // column: 2

    //    foreach (var item in new[] { splitter }.Select((val, index) => new { index, val }))
    //    {
    //        Grid.SetColumn(item.val, item.index);
    //        bodyGrid.Children.Add(item.val);
    //    }

    //    mainGrid.Children.Add(bodyGrid);
    //}

    //private void CreateFooter(Grid mainGrid)
    //{
    //    Border footerBorder = new Border
    //    {
    //        Padding = new Thickness(10, 4)
    //    };

    //    DockPanel footerPanel = new DockPanel()
    //    {
    //        LastChildFill = false,
    //        HorizontalAlignment = Layout.HorizontalAlignment.Right
    //    };

    //    Button okButton = new Button
    //    {
    //        Content = "Ok",
    //        Classes = { "settings-btn-base" }
    //    };

    //    Button cancelButton = new Button
    //    {
    //        Content = "Cancel",
    //        Classes = { "settings-btn-base" }
    //    };

    //    footerPanel.Children.Add(okButton);

    //    footerPanel.Children.Add(cancelButton);

    //    footerBorder.Child = footerPanel;

    //    Grid.SetRow(footerBorder, 1);

    //    mainGrid.Children.Add(footerBorder);
    //}
}

//IControlFactory factory = factoryRegistry.GetFactory(metadata.ControlType);

//Control control = factory.CreateControl(metadata);

//IBindingStrategy bindingStrategy = bindingStrategyRegistry.GetStrategy(metadata.ControlType);

//control.DataContext = settings;

//Data.Binding binding = new Data.Binding(metadata.PropertyInfo.Name, BindingMode.TwoWay);

//bindingStrategy.ApplyBinding(binding, control, metadata);

//return control;