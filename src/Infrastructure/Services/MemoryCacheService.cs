using Application.Interfaces.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Services;

public class MemoryCacheService(IMemoryCache cache, TimeSpan? expiration = null) : ICacheService
{
    private readonly TimeSpan _defaultExpiration = expiration ?? TimeSpan.FromSeconds(60);

    public T? Get<T>(string key) => cache.TryGetValue(key, out T? value) ? value : default;

    public void Remove(string key) => cache.Remove(key);

    public void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = expiration ?? _defaultExpiration,
        };

        cache.Set(key, value, options);
    }
}