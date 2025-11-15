using System.Reflection;

using Avalonia.Controls;
using Avalonia.DynamicSettingsUI.Core.Binding;
using Avalonia.DynamicSettingsUI.Core.Controls;
using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Metadata;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core;

public class SettingsControlFactory
{
    private static readonly SettingsControlBuilder _builder = new SettingsControlBuilder(
        new ControlFactoryRegistry(),
        new BindingStrategyRegistry()
    );

    public static Control CreateSettingsControl(Assembly assembly)
    {
        Type[] settingsTypes = [.. assembly.GetExportedTypes().Where(t => typeof(SettingsBase).IsAssignableFrom(t) && !t.IsAbstract)];

        List<SettingsBase> settingsInstances = [];

        foreach (Type type in settingsTypes)
        {
            if (Activator.CreateInstance(type) is SettingsBase settingsBase)
            {
                settingsBase.Load();
                settingsInstances.Add(settingsBase);
            }
        }

        return CreateSettingsControl(settingsInstances);
    }

    private static Control CreateSettingsControl(List<SettingsBase> settingsGroups)
    {
        List<SettingsGroupLayer> groupLayers = [];

        foreach (var settings in settingsGroups)
        {
            string groupName = settings.GetType().Name.Replace("Settings", "");

            groupLayers.Add(new SettingsGroupLayer
            {
                Name = groupName,
                Metadatas = MetadataReader.GetMetaData(settings),
                SettingsInstance = settings
            });
        }

        return _builder.BuildControl(groupLayers);
    }
}