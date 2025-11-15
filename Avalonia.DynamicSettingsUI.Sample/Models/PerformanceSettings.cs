using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Attributes;

using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System;

namespace Avalonia.DynamicSettingsUI.Sample.Models;

public partial class PerformanceSettings : SettingsBase
{
    [property: Category("Memory")]
    [property: Display("Max Memory Usage", "Maximum memory usage in MB")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(2048)]
    [property: Validation(ValidationType.Range, new object[] { 512, 8192 })]
    [ObservableProperty] private int _maxMemoryUsage = 2048;

    [property: Category("Memory")]
    [property: Display("Enable Aggressive GC", "Use aggressive garbage collection")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(false)]
    [ObservableProperty] private bool _enableAggressiveGC = false;

    [property: Category("CPU")]
    [property: Display("Max CPU Usage", "Maximum CPU usage percentage")]
    [property: ControlType(ControlType.Slider)]
    [property: Value(80)]
    [property: Validation(ValidationType.Range, new object[] { 25, 100 })]
    [ObservableProperty] private int _maxCpuUsage = 80;

    [property: Category("CPU")]
    [property: Display("Worker Threads", "Number of background worker threads")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(4)]
    [property: Validation(ValidationType.Range, new object[] { 1, 16 })]
    [ObservableProperty] private int _workerThreads = 4;

    [property: Category("Cache")]
    [property: Display("Enable File Cache", "Cache frequently accessed files")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _enableFileCache = true;

    [property: Category("Cache")]
    [property: Display("Cache Size", "Cache size in MB")]
    [property: ControlType(ControlType.Numeric)]
    [property: Value(256)]
    [property: Validation(ValidationType.Range, new object[] { 64, 1024 })]
    [ObservableProperty] private int _cacheSize = 256;

    [property: Category("Rendering")]
    [property: Display("Hardware Acceleration", "Use GPU for rendering")]
    [property: ControlType(ControlType.Toggle)]
    [property: Value(true)]
    [ObservableProperty] private bool _hardwareAcceleration = true;

    [property: Category("Rendering")]
    [property: Display("Frame Rate Limit", "Maximum frames per second")]
    [property: ControlType(ControlType.Dropdown)]
    [property: Options("30", "60", "120", "Unlimited")]
    [property: Value("60")]
    [ObservableProperty] private string _frameRateLimit = "60";

    public override string PathToSave => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(PerformanceSettings));

    public override void ApplySettings(object? deserialized)
    {
        if (deserialized is PerformanceSettings settings)
        {
            MaxMemoryUsage = settings.MaxMemoryUsage;
            EnableAggressiveGC = settings.EnableAggressiveGC;
            MaxCpuUsage = settings.MaxCpuUsage;
            WorkerThreads = settings.WorkerThreads;
            EnableFileCache = settings.EnableFileCache;
            CacheSize = settings.CacheSize;
            HardwareAcceleration = settings.HardwareAcceleration;
            FrameRateLimit = settings.FrameRateLimit;
        }
    }
}
