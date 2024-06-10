using CleanProject.CoreApplication.Infrastructure;
using CleanProject.CoreApplication.Infrastructure.Caching;
using CleanProject.CoreApplication.Infrastructure.Template;
using CleanProject.CoreApplication.Infrastructure.Token;
using CleanProject.Infrastructure.Caching;
using CleanProject.Infrastructure.Email;
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
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
    }

    public static void UseInfrastructure(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
    
    public static void AddCustomLogging(this ConfigureHostBuilder host, IConfiguration configuration, bool isLocal=false, string connectionStringKey="TemplateProjectDb")
    {
        var connectionString = configuration.GetConnectionString(connectionStringKey);
        if (isLocal)
        {
            var path = "Logs/info.log";
            host.UseSerilog(
                (_, logConfiguration) =>
                    logConfiguration
                        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                        .WriteTo.File(path, restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                        .WriteTo.MSSqlServer(connectionString: connectionString, restrictedToMinimumLevel: LogEventLevel.Warning, sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "LogEvents",
                            AutoCreateSqlDatabase = false
                        }));
        }
        else
        {
            var path = " Logs/info.log";
            host.UseSerilog(
                (context, logConfiguration) =>
                    logConfiguration
                        .WriteTo.File(path, restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
                        .WriteTo.MSSqlServer(connectionString: connectionString, restrictedToMinimumLevel: LogEventLevel.Warning, sinkOptions: new MSSqlServerSinkOptions
                        {
                            TableName = "LogEvents",
                            AutoCreateSqlDatabase = false
                        }));
        }
        
    }


}