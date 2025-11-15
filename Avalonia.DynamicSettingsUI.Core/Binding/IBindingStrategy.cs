using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Binding;

public interface IBindingStrategy
{
    ControlType ControlType { get; }
    void ApplyBinding(Data.Binding binding, Avalonia.Controls.Control control, SettingsMetadata metadata);
}