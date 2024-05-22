using static TemplateProject.Constants.CacheKeys;

namespace TemplateProject.Infrastructure.Caching;

public interface ICacheService
{
    T Set<T>(CacheKey key, T value);
    bool TryGet<T>(CacheKey key, out T value);
    void Clear(CacheKey key);
    void ClearDomain(Domain domain);
}