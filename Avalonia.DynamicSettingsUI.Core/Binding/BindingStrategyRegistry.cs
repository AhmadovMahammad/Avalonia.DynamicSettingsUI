using Avalonia.DynamicSettingsUI.Core.Binding.Strategies;
using Avalonia.DynamicSettingsUI.Core.Enums;

namespace Avalonia.DynamicSettingsUI.Core.Binding;

public class BindingStrategyRegistry
{
    private readonly Dictionary<ControlType, IBindingStrategy> _strategies = [];

    public BindingStrategyRegistry()
    {
        RegisterDefaultStrategies();
    }

    private void RegisterDefaultStrategies()
    {
        Register(new TextBoxBindingStrategy());
        
    }

    public void Register(IBindingStrategy strategy)
    {
        _strategies[strategy.ControlType] = strategy;
    }

    public IBindingStrategy GetStrategy(ControlType controlType)
    {
        return _strategies.TryGetValue(controlType, out var strategy) ? strategy : GetStrategy(ControlType.TextBox);
    }
}