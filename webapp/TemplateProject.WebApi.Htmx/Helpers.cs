using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.WebApi.Htmx;

public static class Helpers
{
    public static void AddHtmxApp(this IServiceCollection services)
    {
        services.AddSession();
        services.AddHttpContextAccessor();
        services.AddRazorPages().WithRazorPagesRoot("/Htmx");
        
    }
    public static void UseHtmxRoutes(this WebApplication app)
    {
        app.MapRazorPages();
        //app.MapFallbackToAreaPage("/Htmx/{*htmxAppPath}", "/Index", area: "Htmx");
    }
}