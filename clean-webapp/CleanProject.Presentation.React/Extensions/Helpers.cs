using System.Text;
using System.Text.Json;
using CleanProject.CoreApplication.Features.Books;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanProject.Presentation.React.Extensions;

public static class Helpers
{
    private const string AreaName = "React";
    private const string ManifestPath = ".vite/manifest.json";
    private const string ClassLibPath = "CleanProject.Presentation.React";
    private const int DevPort = 5173;
    private static JsonDocument? _manifestDoc;
    
    public static void AddReactApp(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRazorPages().WithRazorPagesRoot("/React");
        services.AddScoped<CommandHandler>();
        services.AddScoped<QueryHandler>();
    }

    public static void UseReactRoutes(this WebApplication app)
    {
        app.MapRazorPages();
        app.MapFallbackToAreaPage("/React/{*reactAppPath}", "/Index", area: AreaName);
    }
    
    public static IHtmlContent RenderStyles(this IWebHostEnvironment hostingEnvironment, string key)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            return new HtmlString($"<!-- Development: Nothing to render.-->");
        }
        var manifest = GetManifestData(hostingEnvironment);
        if (manifest.HasValue && manifest.Value.TryGetProperty(key, out var keyValue) && keyValue.TryGetProperty("css", out JsonElement cssValue) && cssValue.ValueKind == JsonValueKind.Array)
        {
            var stringBuilder = new StringBuilder();
            
            foreach (var cssPath in cssValue.EnumerateArray())
            {
                stringBuilder.AppendLine($"<link rel=\"stylesheet\" href=\"/_content/{ClassLibPath}/{cssPath.GetString()}\">");
            }
            return new HtmlString(stringBuilder.ToString());
        }
        return new HtmlString($"<!-- Failed to render {key}-->");
    }

    public static IHtmlContent RenderScripts(this IWebHostEnvironment hostingEnvironment, string key)
    {
        var manifest = GetManifestData(hostingEnvironment);
        if (manifest.HasValue && manifest.Value.TryGetProperty(key, out JsonElement keyValue) && keyValue.TryGetProperty("file", out JsonElement fileValue) && fileValue.ValueKind == JsonValueKind.String)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(
                    $$"""
                    <script type="module">
                      import RefreshRuntime from 'http://localhost:{{DevPort}}/_content/{{ClassLibPath}}/@react-refresh'
                      RefreshRuntime.injectIntoGlobalHook(window)
                      window.$RefreshReg$ = () => {}
                      window.$RefreshSig$ = () => (type) => type
                      window.__vite_plugin_react_preamble_installed__ = true
                    </script>
                    """);
                stringBuilder.AppendLine($"""<script type="module" src="http://localhost:{DevPort}/_content/{ClassLibPath}/@vite/client"></script>""");
                stringBuilder.AppendLine(
                    $"""<script type="module" src="http://localhost:{DevPort}/_content/{ClassLibPath}/{key}"></script>""");
                return new HtmlString(stringBuilder.ToString());
            }

            var scriptPath = fileValue.GetString();
            return new HtmlString($"<script type=\"module\" src=\"/_content/{ClassLibPath}/{scriptPath}\"></script>");
        }
        return new HtmlString($"<!-- Failed to render {key}-->");
    }


    private static JsonElement? GetManifestData(IWebHostEnvironment hostingEnvironment)
    {
        if (_manifestDoc != null) return _manifestDoc.RootElement;
        Console.WriteLine("HEEEEEELLLLLLOOOOOOOO");
        Console.WriteLine(hostingEnvironment.ContentRootPath);
        var manifestFilePath = hostingEnvironment.IsDevelopment()
            ? Path.Combine(hostingEnvironment.ContentRootPath, $"../{ClassLibPath}/wwwroot", ManifestPath)
            : Path.Combine(hostingEnvironment.ContentRootPath, $"wwwroot/_content/{ClassLibPath}", "manifest.json");

        if (File.Exists(manifestFilePath))
        {
            var manifestJson = File.ReadAllText(manifestFilePath);
            _manifestDoc = JsonDocument.Parse(manifestJson);
            var root = _manifestDoc.RootElement;
            return root;
        }

        return null;
    }
}