using System.Collections.Generic;

namespace Avalonia.RedditMVVM.Services;

public class SettingsService : ISettingsService
{
    private readonly Dictionary<string, object> storage = new();

    public T? GetValue<T>(string key) => storage.TryGetValue(key, out var value) ? (T)value : default;

    public void SetValue<T>(string key, T value)
    {
        if (value is not null)
        {
            storage[key] = value;
        }
    }
}
