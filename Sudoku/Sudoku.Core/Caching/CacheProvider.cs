using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Sudoku.Core.Caching;

public class CacheProvider : ICacheProvider
{
    private readonly IDistributedCache _cache;

    public CacheProvider(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task ClearCache(Guid key)
    {
        await _cache.RemoveAsync(key.ToString());
    }

    public async Task<T> GetFromCache<T>(Guid key) where T : class
    {
        var cachedResponse = await _cache.GetStringAsync(key.ToString());
        return cachedResponse == null ? null : JsonSerializer.Deserialize<T>(cachedResponse);
    }

    public async Task SetCache<T>(Guid key, T value, DistributedCacheEntryOptions options) where T : class
    {
        var response = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key.ToString(), response, options);
    }
}
