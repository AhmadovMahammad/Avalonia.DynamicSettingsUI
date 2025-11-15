using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class NetworkSettings : SettingsBase
{
    [property: Category("Proxy")]
    [property: Display("Enable Proxy", "Use proxy server for connections")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _enableProxy = false;

    [property: Category("Proxy")]
    [property: Display("Proxy URL", "HTTP/HTTPS proxy server address")]
    [property: ControlType(ControlType.UrlInput)]
    [property: Value("")]
    [property: Validation(ValidationType.Url, null)]
    [ObservableProperty] private string _proxyUrl = string.Empty;

    [property: Category("Proxy")]
    [property: Display("Proxy Port", "Proxy server port")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(8080)]
    [property: Validation(ValidationType.Range, new object[] { 1, 65535 })]
    [ObservableProperty] private int _proxyPort = 8080;

    [property: Category("Updates")]
    [property: Display("Auto Check Updates", "Automatically check for updates")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _autoCheckUpdates = true;

    [property: Category("Updates")]
    [property: Display("Update Channel", "Release channel for updates")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("Stable", "Beta", "Insider")]
    [property: Value("Stable")]
    [ObservableProperty] private string _updateChannel = "Stable";

    [property: Category("Telemetry")]
    [property: Display("Enable Telemetry", "Send anonymous usage data")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _enableTelemetry = false;

    [property: Category("Telemetry")]
    [property: Display("Crash Reports", "Send crash reports")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _enableCrashReports = true;

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(NetworkSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is NetworkSettings settings)
        {
            EnableProxy = settings.EnableProxy;
            ProxyUrl = settings.ProxyUrl;
            ProxyPort = settings.ProxyPort;
            AutoCheckUpdates = settings.AutoCheckUpdates;
            UpdateChannel = settings.UpdateChannel;
            EnableTelemetry = settings.EnableTelemetry;
            EnableCrashReports = settings.EnableCrashReports;
        }
    }
}
