using Hydro.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.WebApi.Htmx;

public static class Helpers
{
    public static void AddHtmxApp(this IServiceCollection services)
    {
        services.AddHydro();

    }
    public static void UseHtmxRoutes(this WebApplication app)
    {
        app.UseHydro(app.Environment);
        app.MapRazorPages();
        //app.MapFallbackToAreaPage("/Htmx/{*htmxAppPath}", "/Index", area: "Htmx");
    }
}