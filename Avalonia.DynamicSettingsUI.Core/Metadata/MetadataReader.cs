using System.Reflection;

using Avalonia.DynamicSettingsUI.Core.Attributes;
using Avalonia.DynamicSettingsUI.Core.Core;
using Avalonia.DynamicSettingsUI.Core.Enums;
using Avalonia.DynamicSettingsUI.Core.Models;

namespace Avalonia.DynamicSettingsUI.Core.Metadata;

public static class MetadataReader
{
    public static IEnumerable<SettingsMetadata> GetMetaData(SettingsBase settingsBase)
    {
        PropertyInfo[]? propertyInfos = MetadataCache.GetProperties(settingsBase.GetType());

        foreach (PropertyInfo info in propertyInfos)
        {
            string category = info.GetCustomAttribute<CategoryAttribute>()?.Category ?? "General";
            DisplayAttribute? display = info.GetCustomAttribute<DisplayAttribute>();
            ControlType controlType = info.GetCustomAttribute<ControlTypeAttribute>()?.ControlType ?? ControlType.TextBox;
            string[]? options = info.GetCustomAttribute<OptionsAttribute>()?.Options;
            object? value = info.GetCustomAttribute<ValueAttribute>()?.Value;
            object? defaultValue = info.GetCustomAttribute<ValueAttribute>()?.Value;

            yield return new SettingsMetadata
            {
                PropertyInfo = info,
                Category = category,
                Name = display?.Name ?? info.Name,
                Description = display?.Description,
                ControlType = controlType,
                Options = options,
                Value = value,
                DefaultValue = defaultValue
            };
        }
    }
}
