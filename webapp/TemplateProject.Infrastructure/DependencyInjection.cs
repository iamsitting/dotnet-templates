using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using TemplateProject.Infrastructure.Implementations;
using TemplateProject.Infrastructure.Implementations.Models;

namespace TemplateProject.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailOptions>(configuration.GetRequiredSection(EmailOptions.Key));
        services.AddScoped<IEmailService, EmailService>();

        services.AddMemoryCache();
        services.Configure<CacheConfigurationOptions>(configuration.GetRequiredSection(CacheConfigurationOptions.Key));
        services.AddSingleton<ICacheService, MemoryCacheService>();
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