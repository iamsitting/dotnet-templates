using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using TemplateProject.Infrastructure.Caching;
using TemplateProject.Infrastructure.Email;
using TemplateProject.Infrastructure.Templates;
using TemplateProject.Infrastructure.Tokens;

namespace TemplateProject.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetRequiredSection(EmailOptions.Key));
        services.Configure<CacheConfigurationOptions>(configuration.GetRequiredSection(CacheConfigurationOptions.Key));
        services.Configure<TokenOptions>(configuration.GetRequiredSection(TokenOptions.Key));
        
        services.AddScoped<IEmailService, EmailService>();
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.AddScoped<ITemplateService, TemplateService>();
    }
    
    public static void AddCustomLogging(this ConfigureHostBuilder host, IConfiguration configuration, bool isLocal=false, string connectionStringKey="TemplateProjectDb")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);
        if (isLocal)
        {
            host.UseSerilog(
                (_, logConfiguration) =>
                    logConfiguration
                        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                        .WriteTo.File("Logs/info.log", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                        .WriteTo.MSSqlServer(connectionString: connectionString, restrictedToMinimumLevel: LogEventLevel.Warning, sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "LogEvents",
                            AutoCreateSqlDatabase = false
                        }));
        }
        else
        {
            host.UseSerilog(
                (context, logConfiguration) =>
                    logConfiguration
                        .WriteTo.File("Logs/info.log", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                        .WriteTo.MSSqlServer(connectionString: connectionString, restrictedToMinimumLevel: LogEventLevel.Warning, sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "LogEvents",
                            AutoCreateSqlDatabase = false
                        }));
        }
        
    }


}