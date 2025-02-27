
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

public class RedisCacheServices : IRedisCacheServices
{
    private readonly IDistributedCache _cache;

    public RedisCacheServices(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T? GetData<T>(string key)
    {
        var data = _cache?.GetString(key);
        if (data is null)
        {
            return default(T);
        }
        return JsonSerializer.Deserialize<T>(data);
    }

    public void SetData<T>(string key, T value)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };
        var data = JsonSerializer.Serialize<T>(value);
        _cache?.SetString(key, data, options);
    }
}