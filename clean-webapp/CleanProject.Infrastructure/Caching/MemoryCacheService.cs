using System.Collections.Concurrent;
using CleanProject.CoreApplication.Constants;
using CleanProject.CoreApplication.Infrastructure.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CleanProject.Infrastructure.Caching;

internal class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly MemoryCacheEntryOptions _cacheOptions;
    private readonly ConcurrentDictionary<string, byte> _keys;


    public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfigurationOptions> configOptions)
    {
        _memoryCache = memoryCache;
        _cacheOptions = new MemoryCacheEntryOptions();
        _cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(configOptions.Value.AbsoluteExpirationInMinutes));
        _cacheOptions.SetSlidingExpiration(TimeSpan.FromMinutes(configOptions.Value.SlidingExpirationInMinutes));
        _keys = new ConcurrentDictionary<string, byte>();
    }

    public T Set<T>(CacheKey key, T value)
    {
        var cacheKey = key.ToString();
        _keys.TryAdd(cacheKey, 0);
        return _memoryCache.Set(cacheKey, value, _cacheOptions);
    }

    public bool TryGet<T>(CacheKey key, out T value)
    {
        var cacheKey = key.ToString();
        _memoryCache.TryGetValue(cacheKey, out value);
        return value != null;
    }

    public void Clear(CacheKey key)
    {
        var cacheKey = key.ToString();
        _keys.TryRemove(cacheKey, out _);
        _memoryCache.Remove(cacheKey);
        
        var keysToRemove = _keys.Keys.Where(k => k.Contains($":{key.Domain}")).ToList();
        foreach (var k in keysToRemove)
        {
            _memoryCache.Remove(k);
            _keys.TryRemove(k, out _);
        }

    }

    public void ClearDomain(CacheKeys.Domain domain)
    {
        var keysToRemove = _keys.Keys.Where(key => key.StartsWith(domain.ToString()) || key.Contains($":{domain}"))
            .ToList();
        foreach (var key in keysToRemove)
        {
            _memoryCache.Remove(key);
            _keys.TryRemove(key, out _);
        }
    }
}