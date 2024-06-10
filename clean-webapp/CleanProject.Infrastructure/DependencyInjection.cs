using CleanProject.CoreApplication.Infrastructure;
using CleanProject.CoreApplication.Infrastructure.Caching;
using CleanProject.CoreApplication.Infrastructure.Template;
using CleanProject.CoreApplication.Infrastructure.Token;
using CleanProject.Infrastructure.Caching;
using CleanProject.Infrastructure.Email;
using CleanProject.Infrastructure.Logging;
using CleanProject.Infrastructure.Templates;
using CleanProject.Infrastructure.Token;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace CleanProject.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connectionStringKey = "CleanProjectDb", bool isLocal = false)
    {
        services.Configure<EmailOptions>(configuration.GetRequiredSection(EmailOptions.Key));
        services.Configure<CacheConfigurationOptions>(configuration.GetRequiredSection(CacheConfigurationOptions.Key));
        services.Configure<TokenOptions>(configuration.GetRequiredSection(TokenOptions.Key));

        services.AddScoped<IEmailService, EmailService>();
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        services.AddSingleton<ITokenService, TokenService>();

        services.Configure<TemplateOptions>(configuration.GetRequiredSection(TemplateOptions.Key));
        services.AddScoped<ITemplateService, TemplateService>();

        var connectionString = configuration.GetConnectionString(connectionStringKey);
        services.AddLogging(configuration, connectionString, isLocal);
        services.AddSingleton(typeof(IAppLogger<>), typeof(SerilogLogger<>));

    }

    private static void AddLogging(this IServiceCollection services, IConfiguration configuration,  string? connectionString = null, bool isLocal = false)
    {
        var path = "Logs/info.log";
        var loggerConfig = new LoggerConfiguration();
        
        if (isLocal) loggerConfig = loggerConfig.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug);
        loggerConfig = loggerConfig.WriteTo.File(path, restrictedToMinimumLevel: LogEventLevel.Information,
            rollingInterval: RollingInterval.Day);

        if (!string.IsNullOrEmpty(connectionString))
        {
            loggerConfig = loggerConfig.WriteTo.MSSqlServer(connectionString: connectionString,
                restrictedToMinimumLevel: LogEventLevel.Warning,
                sinkOptions: new MSSqlServerSinkOptions
                {
                    TableName = "LogEvents",
                    AutoCreateSqlDatabase = false
                });
        }

        Log.Logger = loggerConfig.CreateLogger();
        
        services.AddSingleton(typeof(IAppLogger<>), typeof(SerilogLogger<>));
    }
}