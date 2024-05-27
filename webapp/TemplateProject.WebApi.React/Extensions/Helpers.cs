using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TemplateProject.WebApi.React.Extensions;

public static class Helpers
{
    private const string AreaName = "React";
    private const string ManifestPath = ".vite/manifest.json";
    private const string ClassLibPath = "TemplateProject.WebApi.React";
    private static JsonDocument? _manifestDoc;
    
    public static void AddReactApp(this IServiceCollection services)
    {
        services.AddSession();
        services.AddHttpContextAccessor();
        services.AddRazorPages();
        services.AddSignalR();
    }
    
    public static IHtmlContent RenderStyles(this IWebHostEnvironment hostingEnvironment, string key)
    {
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
            var scriptPath = fileValue.GetString();
            return new HtmlString($"<script type=\"module\" src=\"/_content/{ClassLibPath}/{scriptPath}\"></script>");
        }
        return new HtmlString($"<!-- Failed to render {key}-->");
    }


    private static JsonElement? GetManifestData(IWebHostEnvironment hostingEnvironment)
    {
        if (_manifestDoc != null) return _manifestDoc.RootElement;
        var manifestFilePath = hostingEnvironment.IsDevelopment()
            ? Path.Combine(hostingEnvironment.WebRootPath, $"../../{ClassLibPath}/wwwroot", ManifestPath)
            : Path.Combine(hostingEnvironment.WebRootPath, $"_content/{ClassLibPath}", "manifest.json");

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