using Microsoft.Extensions.Caching.Memory;
using ShuttleForecast.Application.Common.Contracts;

namespace Infrastructure;

public class InMemoryCache(IMemoryCache cache) : ICache
{
    public string? GetData(string key)
    {
        cache.TryGetValue(key, out string? cachedData);

        return cachedData;
    }

    public void SetData(string key, string data)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        cache.Set(key, data, cacheEntryOptions);
    }
}