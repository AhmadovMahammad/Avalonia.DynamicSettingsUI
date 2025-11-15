using System.Text.Json;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.DynamicSettingsUI.Core.Core;

public abstract class SettingsBase : ObservableObject
{
    public event Action? SettingsSaved;

    public void RaiseSaved()
    {
        SettingsSaved?.Invoke();
    }

    public abstract string PathToSave { get; }

    public abstract void ApplySettings(object? deserialized);

    public virtual void Load()
    {
        if (!File.Exists(PathToSave))
        {
            return;
        }

        string json = File.ReadAllText(PathToSave);

        JsonDocument document = JsonDocument.Parse(json);

        object? deserialized = JsonSerializer.Deserialize(document, GetType());

        ApplySettings(deserialized);
    }

    public virtual void Save(JsonSerializerOptions? options)
    {
        options ??= new JsonSerializerOptions() { WriteIndented = true };

        string json = JsonSerializer.Serialize(this, GetType(), options);

        File.WriteAllText(PathToSave, json);
    }
}
