using System.Text;
using CleanProject.CoreApplication.Constants;

namespace CleanProject.CoreApplication.Infrastructure.Caching;

public class CacheKey
{
    public CacheKeys.Domain Domain { get; init; }
    public CacheKeys.CacheType CacheType { get; init; }
    public string? Value { get; set; }
    public CacheKeys.Domain[] DependencyDomains { get; init; } = [];

    public CacheKey(CacheKeys.Domain domain, CacheKeys.CacheType cacheType, string? value = null, CacheKeys.Domain[]? dependencyDomains = null)
    {
        Domain = domain;
        CacheType = cacheType;
        Value = value;
        if (dependencyDomains != null)
        {
            DependencyDomains = dependencyDomains;
        }
    }

    public override string ToString()
    {
        var key = new StringBuilder(Domain.ToString());
        key.Append('|');
        key.Append(CacheType.ToString());
        if (Value != null)
        {
            key.Append('=');
            key.Append(Value);
        }

        if (DependencyDomains.Length != 0)
        {
            foreach(var domain in DependencyDomains)
            {
                key.Append(':');
                key.Append(domain.ToString());
            }
        }

        return key.ToString();
    }
}