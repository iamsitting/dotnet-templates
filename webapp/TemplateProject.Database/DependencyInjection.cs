using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.Database;

public static class DependencyInjection
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration,
        string connectionStringKey = "TemplateProjectDb")
    {
        services.AddSingleton<IColumnTypes, SqliteColumnTypes>();
        var connectionString = configuration.GetConnectionString(connectionStringKey);
    }
}