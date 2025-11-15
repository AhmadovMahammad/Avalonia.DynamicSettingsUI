using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class AdvancedSettings : SettingsBase
{
    [property: Category("Developer")]
    [property: Display("Developer Mode", "Enable developer features")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _developerMode = false;

    [property: Category("Developer")]
    [property: Display("Show Debug Console", "Display debug console at startup")]
    [property: ControlType(ControlType.CheckBox)]
    [property: Value(false)]
    [ObservableProperty] private bool _showDebugConsole = false;

    [property: Category("Logging")]
    [property: Display("Log Level", "Application logging verbosity")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("Error", "Warning", "Info", "Debug", "Trace")]
    [property: Value("Info")]
    [ObservableProperty] private string _logLevel = "Info";

    [property: Category("Logging")]
    [property: Display("Log Directory", "Location for log files")]
    [property: ControlType(ControlType.FolderPicker)]
    [property: Value("")]
    [ObservableProperty] private string _logDirectory = string.Empty;

    [property: Category("Logging")]
    [property: Display("Max Log Files", "Maximum number of log files to keep")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(10)]
    [property: Validation(ValidationType.Range, new object[] { 1, 100 })]
    [ObservableProperty] private int _maxLogFiles = 10;

    [property: Category("Experimental")]
    [property: Display("Enable Experimental Features", "Use unstable features")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _enableExperimentalFeatures = false;

    [property: Category("Experimental")]
    [property: Display("Beta Channel", "Receive beta updates")]
    [property: ControlType(ControlType.CheckBox)]
    [property: Value(false)]
    [ObservableProperty] private bool _betaChannel = false;

    [property: Category("Data")]
    [property: Display("Data Retention Period", "Days to retain data")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(90)]
    [property: Validation(ValidationType.Range, new object[] { 7, 365 })]
    [ObservableProperty] private int _dataRetentionDays = 90;

    [property: Category("Data")]
    [property: Display("Auto Cleanup", "Automatically cleanup old data")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _autoCleanup = true;

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(AdvancedSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is AdvancedSettings settings)
        {
            DeveloperMode = settings.DeveloperMode;
            ShowDebugConsole = settings.ShowDebugConsole;
            LogLevel = settings.LogLevel;
            LogDirectory = settings.LogDirectory;
            MaxLogFiles = settings.MaxLogFiles;
            EnableExperimentalFeatures = settings.EnableExperimentalFeatures;
            BetaChannel = settings.BetaChannel;
            DataRetentionDays = settings.DataRetentionDays;
            AutoCleanup = settings.AutoCleanup;
        }
    }
}