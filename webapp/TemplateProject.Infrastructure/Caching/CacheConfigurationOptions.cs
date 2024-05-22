namespace TemplateProject.Infrastructure.Caching;

internal class CacheConfigurationOptions
{
    public const string Key = "CacheConfigurationOptions";
    public int AbsoluteExpirationInMinutes { get; set; }
    public int SlidingExpirationInMinutes { get; set; }
}