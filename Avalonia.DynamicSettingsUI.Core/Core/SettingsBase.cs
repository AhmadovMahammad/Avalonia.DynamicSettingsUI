using System.Text.Json;

namespace Avalonia.DynamicSettingsUI.Core.Core;

public abstract class SettingsBase
{
    public event Action? SettingsSaved;

    public void RaiseSaved()
    {
        SettingsSaved?.Invoke();
    }

    public abstract string PathToSave { get; }

    public virtual T? Load<T>() where T : SettingsBase
    {
        if (!File.Exists(PathToSave))
        {
            return null;
        }

        string json = File.ReadAllText(PathToSave);

        return JsonSerializer.Deserialize<T>(json);
    }

    public virtual void Save(JsonSerializerOptions? options)
    {
        options ??= new JsonSerializerOptions() { WriteIndented = true };

        string json = JsonSerializer.Serialize(this, GetType(), options);

        File.WriteAllText(PathToSave, json);
    }
}
