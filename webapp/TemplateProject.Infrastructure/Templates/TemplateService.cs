using Microsoft.Extensions.Hosting;
using TemplateProject.Constants;
using TemplateProject.Infrastructure.Caching;

namespace TemplateProject.Infrastructure.Templates;

internal class TemplateService : ITemplateService
{
    private readonly string _basePath;
    private readonly ICacheService _cacheService;

    public TemplateService(IHostEnvironment environment, ICacheService cacheService)
    {
        _basePath = environment.IsDevelopment()
            ? "../TemplateProject.Infrastructure/AppData"
            : "./AppData";
        _cacheService = cacheService;
    }

    public async Task<string> GetTemplateFromParametersAsync<T>(T parameters,
        CancellationToken cancellationToken = default) where T : ITemplateParameters
    {
        
        var key = new CacheKey(CacheKeys.Domain.Templates, CacheKeys.CacheType.Name, parameters.FilePath);
        var success = _cacheService.TryGet(key, out string template);
        if (success) return parameters.ParseTemplateString(template);
        
        var filePath = Path.Combine(_basePath, parameters.FilePath);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                $"File does not exist at {filePath}");
        }
        
        template = await File.ReadAllTextAsync(filePath, cancellationToken);
        _cacheService.Set(key, template);

        return parameters.ParseTemplateString(template);
    }
    
    public string GetTemplateFromParameters<T>(T parameters) where T : ITemplateParameters
    {
        
        var key = new CacheKey(CacheKeys.Domain.Templates, CacheKeys.CacheType.Name, parameters.FilePath);
        var success = _cacheService.TryGet(key, out string template);
        if (success) return parameters.ParseTemplateString(template);
        
        var filePath = Path.Combine(_basePath, parameters.FilePath);
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException(
                $"File does not exist at {filePath}");
        }
        
        template = File.ReadAllText(filePath);
        _cacheService.Set(key, template);

        return parameters.ParseTemplateString(template);
    }
    
}