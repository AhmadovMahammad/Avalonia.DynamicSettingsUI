
using Avalonia.DynamicSettingsUI.Core.Controls.Factories;
using Avalonia.DynamicSettingsUI.Core.Enums;

namespace Avalonia.DynamicSettingsUI.Core.Controls;

public class ControlFactoryRegistry
{
    private readonly Dictionary<ControlType, IControlFactory> _factories = [];

    public ControlFactoryRegistry()
    {
        RegisterDefaultFactories();
    }

    private void RegisterDefaultFactories()
    {
        Register(new TextBoxFactory());
    }

    public void Register(IControlFactory factory)
    {
        _factories[factory.ControlType] = factory;
    }

    public IControlFactory GetFactory(ControlType controlType)
    {
        return _factories.TryGetValue(controlType, out var factory) ? factory : GetFactory(ControlType.TextBox);
    }
}
