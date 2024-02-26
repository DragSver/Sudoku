using Microsoft.Extensions.Caching.Distributed;

namespace Sudoku.Core.Caching;

public interface ICacheProvider
{
    Task<T> GetFromCache<T>(Guid key) where T : class;
    Task SetCache<T>(Guid key, T value, DistributedCacheEntryOptions options) where T : class;
    Task ClearCache(Guid key);
}
