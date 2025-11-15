using Avalonia.DynamicSettingsUI.Core.Core;

using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.DynamicSettingsUI.Core.Enums;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class AppearanceSettings : SettingsBase
{
    [property: Category("Theme")]
    [property: Display("Theme Mode", "Application color theme")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("Light", "Dark", "Auto")]
    [property: Value("Dark")]
    [ObservableProperty] private string _theme = "Dark";

    [property: Category("Theme")]
    [property: Display("Accent Color", "Primary accent color used throughout the UI")]
    [property: ControlType(ControlType.ColorPicker)]
    [property: Value("#0078D4")]
    [ObservableProperty] private string _accentColor = "#0078D4";

    [property: Category("Font")]
    [property: Display("Font Family", "Primary font used in the editor")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("Segoe UI", "Consolas", "Cascadia Code", "JetBrains Mono", "Fira Code")]
    [property: Value("Segoe UI")]
    [ObservableProperty] private string _fontFamily = "Segoe UI";

    [property: Category("Font")]
    [property: Display("Font Size", "Base font size in pixels")]
    [property: ControlType(ControlType.Slider)]
    [property: Value(14)]
    [property: Validation(ValidationType.Range, new object[10, 24])]
    [ObservableProperty] private int _fontSize = 14;

    [property: Category("Window")]
    [property: Display("Window Transparency", "Opacity level of the main window")]
    [property: ControlType(ControlType.Slider)]
    [property: Value(100)]
    [property: Validation(ValidationType.Range, new object[70, 100])]
    [ObservableProperty] private int _windowOpacity = 100;

    [property: Category("Window")]
    [property: Display("Enable Animations", "Smooth UI transitions and animations")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _enableAnimations = true;

    [property: Category("Layout")]
    [property: Display("Sidebar Width", "Width of the sidebar in pixels")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(250)]
    [property: Validation(ValidationType.Range, new object[150, 500])]
    [ObservableProperty] private int _sidebarWidth = 250;

    [property: Category("Layout")]
    [property: Display("Compact Mode", "Use compact spacing for UI elements")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _compactMode = false;

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(AppearanceSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is AppearanceSettings settings)
        {
            Theme = settings.Theme;
            AccentColor = settings.AccentColor;
            FontFamily = settings.FontFamily;
            FontSize = settings.FontSize;
            WindowOpacity = settings.WindowOpacity;
            EnableAnimations = settings.EnableAnimations;
            SidebarWidth = settings.SidebarWidth;
            CompactMode = settings.CompactMode;
        }
    }
}