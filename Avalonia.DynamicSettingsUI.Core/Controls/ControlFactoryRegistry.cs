
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
        Register(new DropdownFactory());
        Register(new NumericFactory());
        Register(new PasswordBoxFactory());
        Register(new SliderFactory());
        Register(new TextBoxFactory());
        Register(new ToggleFactory());
        Register(new FilePickerFactory(true));
        Register(new FilePickerFactory(false));
        Register(new FolderPickerFactory());
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
