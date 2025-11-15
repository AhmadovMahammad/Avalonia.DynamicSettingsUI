using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class EditorSettings : SettingsBase
{
    [property: Category("Editing")]
    [property: Display("Tab Size", "Number of spaces per tab")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(4)]
    [property: Validation(ValidationType.Range, new object[2, 8])]
    [ObservableProperty] private int _tabSize = 4;

    [property: Category("Editing")]
    [property: Display("Insert Spaces", "Use spaces instead of tabs")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _insertSpaces = true;

    [property: Category("Editing")]
    [property: Display("Word Wrap", "Enable word wrapping in editor")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("Off", "On", "Bounded")]
    [property: Value("Off")]
    [ObservableProperty] private string _wordWrap = "Off";

    [property: Category("Editing")]
    [property: Display("Auto Save", "Automatically save files")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("Off", "After Delay", "On Focus Change")]
    [property: Value("After Delay")]
    [ObservableProperty] private string _autoSave = "After Delay";

    [property: Category("Display")]
    [property: Display("Line Numbers", "Show line numbers")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _lineNumbers = true;

    [property: Category("Display")]
    [property: Display("Minimap", "Show code minimap")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _minimap = true;

    [property: Category("Display")]
    [property: Display("Render Whitespace", "Show whitespace characters")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("None", "Boundary", "Selection", "All")]
    [property: Value("Selection")]
    [ObservableProperty] private string _renderWhitespace = "Selection";

    [property: Category("Behavior")]
    [property: Display("Format On Save", "Automatically format files on save")]
    [property: ControlType(ControlType.CheckBox)]
    [property: Value(true)]
    [ObservableProperty] private bool _formatOnSave = true;

    [property: Category("Behavior")]
    [property: Display("Trim Trailing Whitespace", "Remove trailing spaces on save")]
    [property: ControlType(ControlType.CheckBox)]
    [property: Value(true)]
    [ObservableProperty] private bool _trimTrailingWhitespace = true;

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(EditorSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is EditorSettings settings)
        {
            TabSize = settings.TabSize;
            InsertSpaces = settings.InsertSpaces;
            WordWrap = settings.WordWrap;
            AutoSave = settings.AutoSave;
            LineNumbers = settings.LineNumbers;
            Minimap = settings.Minimap;
            RenderWhitespace = settings.RenderWhitespace;
            FormatOnSave = settings.FormatOnSave;
            TrimTrailingWhitespace = settings.TrimTrailingWhitespace;
        }
    }
}
