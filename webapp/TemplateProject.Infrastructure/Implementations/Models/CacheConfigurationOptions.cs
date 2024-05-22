namespace TemplateProject.Infrastructure.Implementations.Models;

public class CacheConfigurationOptions
{
    public const string Key = "CacheConfigurationOptions";
    public int AbsoluteExpirationInMinutes { get; set; }
    public int SlidingExpirationInMinutes { get; set; }
}