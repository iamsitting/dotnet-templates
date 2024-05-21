using System.Text;
using static TemplateProject.Constants.CacheKeys;

namespace TemplateProject.Infrastructure;

public class CacheKey
{
    public Domain Domain { get; init; }
    public CacheType CacheType { get; init; }
    public string? Value { get; set; }
    public Domain[] DependencyDomains { get; init; } = [];

    public CacheKey(Domain domain, CacheType cacheType, string? value = null, Domain[]? dependencyDomains = null)
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