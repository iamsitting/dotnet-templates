using CleanProject.CoreApplication.Domain.Books;
using CleanProject.Persistence.EF;
using CleanProject.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanProject.Persistence;

public static class DependencyInjection
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration,
        string connectionStringKey = "CleanProjectDb")
    {
        services.AddSingleton<IColumnTypes, SqliteColumnTypes>();
        var connectionString = configuration.GetConnectionString(connectionStringKey);
        services.AddDbContext<CleanProjectContext>(options => options.UseSqlite(connectionString));

        services.AddScoped<IBookRepository, BookRepository>();
    }
}