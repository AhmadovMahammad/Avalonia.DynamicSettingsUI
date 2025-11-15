using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;

namespace Avalonia.DynamicSettingsUI.Core.Controls;

public interface IControlFactory
{
    ControlType ControlType { get; }
    Control CreateControl();
}
