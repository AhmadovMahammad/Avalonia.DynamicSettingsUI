using System.Reflection;

namespace Avalonia.DynamicSettingsUI.Core.Metadata;

public static class MetadataCache
{
    private static readonly Dictionary<Type, PropertyInfo[]> _cache = [];

    public static PropertyInfo[] GetProperties(Type type)
    {
        if (!_cache.TryGetValue(type, out PropertyInfo[]? properties))
        {
            properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            _cache.Add(type, properties);
        }

        return properties;
    }
}
