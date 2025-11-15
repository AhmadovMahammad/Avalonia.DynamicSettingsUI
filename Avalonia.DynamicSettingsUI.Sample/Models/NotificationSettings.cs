using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class NotificationSettings : SettingsBase
{
    [property: Category("General")]
    [property: Display("Enable Notifications", "Show desktop notifications")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _enableNotifications = true;

    [property: Category("General")]
    [property: Display("Play Sounds", "Play notification sounds")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _playSounds = true;

    [property: Category("Types")]
    [property: Display("Show Info Messages", "Display informational notifications")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _showInfo = true;

    [property: Category("Types")]
    [property: Display("Show Warnings", "Display warning notifications")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _showWarnings = true;

    [property: Category("Types")]
    [property: Display("Show Errors", "Display error notifications")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _showErrors = true;

    [property: Category("Behavior")]
    [property: Display("Auto Dismiss Duration", "Automatically close notifications after (seconds)")]
    [property: ControlType(ControlType.Slider)]
    [property: Value(5)]
    [property: Validation(ValidationType.Range, new object[] { 3, 30 })]
    [ObservableProperty] private int _autoDismissSeconds = 5;

    [property: Category("Behavior")]
    [property: Display("Do Not Disturb", "Suppress all notifications")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _doNotDisturb = false;

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(NotificationSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is NotificationSettings settings)
        {
            EnableNotifications = settings.EnableNotifications;
            PlaySounds = settings.PlaySounds;
            ShowInfo = settings.ShowInfo;
            ShowWarnings = settings.ShowWarnings;
            ShowErrors = settings.ShowErrors;
            AutoDismissSeconds = settings.AutoDismissSeconds;
            DoNotDisturb = settings.DoNotDisturb;
        }
    }
}
