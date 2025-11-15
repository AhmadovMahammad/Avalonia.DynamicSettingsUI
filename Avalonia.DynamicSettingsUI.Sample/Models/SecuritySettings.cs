using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class SecuritySettings : SettingsBase
{
    [property: Category("Authentication")]
    [property: Display("Remember Credentials", "Save login credentials")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _rememberCredentials = true;

    [property: Category("Authentication")]
    [property: Display("Session Timeout", "Automatic logout after inactivity (minutes)")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(30)]
    [property: Validation(ValidationType.Range, new object[] { 5, 240 })]
    [ObservableProperty] private int _sessionTimeout = 30;

    [property: Category("Encryption")]
    [property: Display("Encrypt Local Files", "Encrypt sensitive data at rest")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _encryptLocalFiles = false;

    [property: Category("Encryption")]
    [property: Display("Encryption Algorithm", "Algorithm for encryption")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("AES-256", "AES-128", "ChaCha20")]
    [property: Value("AES-256")]
    [ObservableProperty] private string _encryptionAlgorithm = "AES-256";

    [property: Category("Permissions")]
    [property: Display("Allow External Extensions", "Allow third-party extensions")]
    [property: ControlType(ControlType.CheckBox)]
    [property: Value(true)]
    [ObservableProperty] private bool _allowExternalExtensions = true;

    [property: Category("Permissions")]
    [property: Display("Enable Script Execution", "Allow running scripts")]
    [property: ControlType(ControlType.CheckBox)]
    [property: Value(false)]
    [ObservableProperty] private bool _enableScriptExecution = false;

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(SecuritySettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is SecuritySettings settings)
        {
            RememberCredentials = settings.RememberCredentials;
            SessionTimeout = settings.SessionTimeout;
            EncryptLocalFiles = settings.EncryptLocalFiles;
            EncryptionAlgorithm = settings.EncryptionAlgorithm;
            AllowExternalExtensions = settings.AllowExternalExtensions;
            EnableScriptExecution = settings.EnableScriptExecution;
        }
    }
}
