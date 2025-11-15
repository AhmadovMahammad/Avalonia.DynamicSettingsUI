

using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Binding;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Controls;

internal sealed class SettingsControlBuilder(
    ControlFactoryRegistry factoryRegistry,
    BindingStrategyRegistry bindingStrategyRegistry)
{
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

        CreateFooter(mainGrid);

        return mainGrid;

        //IControlFactory factory = factoryRegistry.GetFactory(metadata.ControlType);

        //Control control = factory.CreateControl(metadata);

        //IBindingStrategy bindingStrategy = bindingStrategyRegistry.GetStrategy(metadata.ControlType);

        //control.DataContext = settings;

        //Data.Binding binding = new Data.Binding(metadata.PropertyInfo.Name, BindingMode.TwoWay);

        //bindingStrategy.ApplyBinding(binding, control, metadata);

        //return control;
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

        // column: 0

        // column: 1
        GridSplitter splitter = new GridSplitter
        {
            Classes = { "settings-splitter" },
            ShowsPreview = true,
            ResizeBehavior = GridResizeBehavior.BasedOnAlignment,
            ResizeDirection = GridResizeDirection.Columns
        };

        // column: 2

        foreach (var item in new[] { splitter }.Select((val, index) => new { index, val }))
        {
            Grid.SetColumn(item.val, item.index);
            bodyGrid.Children.Add(item.val);
        }

        mainGrid.Children.Add(bodyGrid);
    }

    private void CreateFooter(Grid mainGrid)
    {
    }
}