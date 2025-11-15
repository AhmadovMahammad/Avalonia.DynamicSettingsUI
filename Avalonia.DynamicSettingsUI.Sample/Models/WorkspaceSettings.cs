using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class WorkspaceSettings : SettingsBase
{
    [property: Category("Paths")]
    [property: Display("Default Project Location", "Default folder for new projects")]
    [property: ControlType(ControlType.FolderPicker)]
    [property: Value("")]
    [property: Validation(ValidationType.NotEmpty, null)]
    [ObservableProperty] private string _defaultProjectLocation = string.Empty;

    [property: Category("Paths")]
    [property: Display("Recent Projects Limit", "Maximum number of recent projects to remember")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(10)]
    [property: Validation(ValidationType.Range, new object[] { 5, 50 })]
    [ObservableProperty] private int _recentProjectsLimit = 10;

    [property: Category("Startup")]
    [property: Display("Restore Previous Session", "Reopen files from last session")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _restorePreviousSession = true;

    [property: Category("Startup")]
    [property: Display("Open Folder On Launch", "Behavior when launching without a folder")]
    [property: ControlType(ControlType.RadioGroup)]
    [property: Options("Empty Window", "Welcome Screen", "Last Folder")]
    [property: Value("Welcome Screen")]
    [ObservableProperty] private string _openFolderOnLaunch = "Welcome Screen";

    [property: Category("Files")]
    [property: Display("Auto Detect Encoding", "Automatically detect file encoding")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _autoDetectEncoding = true;

    [property: Category("Files")]
    [property: Display("Default Encoding", "Encoding for new files")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("UTF-8", "UTF-16", "ASCII", "Windows-1252")]
    [property: Value("UTF-8")]
    [ObservableProperty] private string _defaultEncoding = "UTF-8";

    [property: Category("Files")]
    [property: Display("Exclude Patterns", "File patterns to exclude from explorer")]
    [property: ControlType(ControlType.TextBox)]
    [property: Value("**/node_modules, **/.git, **/bin, **/obj")]
    [ObservableProperty] private string _excludePatterns = "**/node_modules, **/.git, **/bin, **/obj";

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(WorkspaceSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is WorkspaceSettings settings)
        {
            DefaultProjectLocation = settings.DefaultProjectLocation;
            RecentProjectsLimit = settings.RecentProjectsLimit;
            RestorePreviousSession = settings.RestorePreviousSession;
            OpenFolderOnLaunch = settings.OpenFolderOnLaunch;
            AutoDetectEncoding = settings.AutoDetectEncoding;
            DefaultEncoding = settings.DefaultEncoding;
            ExcludePatterns = settings.ExcludePatterns;
        }
    }
}
