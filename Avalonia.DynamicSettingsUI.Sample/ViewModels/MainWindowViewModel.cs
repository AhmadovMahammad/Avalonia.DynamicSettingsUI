using System.Linq;
using System.Reflection;

using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.DynamicSettingsUI.Core;
using Avalonia.DynamicSettingsUI.Sample.Models;

using CommunityToolkit.Mvvm.Input;

namespace Avalonia.DynamicSettingsUI.Sample.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [RelayCommand]
    public void OpenSettings()
    {
        Assembly settingsAssembly = typeof(AppearanceSettings).Assembly;

        Control control = SettingsControlFactory.CreateSettingsControl(settingsAssembly);

        if (Application.Current is IClassicDesktopStyleApplicationLifetime desktop && desktop.Windows.FirstOrDefault(w => w is SettingsWindow) is Window window)
        {
            window.Content = control;
            return;
        }

        SettingsWindow settingsWindow = new SettingsWindow
        {
            Content = control
        };

        settingsWindow.Show();
    }
}
