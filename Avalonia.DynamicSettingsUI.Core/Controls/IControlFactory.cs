using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Controls;

public interface IControlFactory
{
    ControlType ControlType { get; }
    Control CreateControl(SettingsMetadata metadata);
}
