using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TemplateProject.WebApi.React.Extensions;

public static class Helpers
{
    private const string AreaName = "React";
    public const string ManifestPath = ".vite/manifest.json";
    
    public static void AddReactApp(this IServiceCollection services)
    {
        services.AddSession();
        services.AddHttpContextAccessor();
        services.AddRazorPages();
        services.AddSignalR();
    }
    
    public static IHtmlContent RenderStyles(this IHtmlHelper htmlHelper, string key, IWebHostEnvironment hostingEnvironment)
    {
        var manifest = GetManifestData(hostingEnvironment);
        if (manifest != null && manifest.ContainsKey(key) && manifest[key].ContainsKey("file"))
        {
            var cssPath = manifest[key]["file"].ToString();
            return new HtmlString($"<link rel=\"stylesheet\" href=\"/_content/React/{cssPath}\">");
        }
        return new HtmlString($"<!-- Failed to render {key}-->");
    }

    public static IHtmlContent RenderScripts(this IHtmlHelper htmlHelper, string key, IWebHostEnvironment hostingEnvironment)
    {
        var manifest = GetManifestData(hostingEnvironment);
        if (manifest != null && manifest.ContainsKey(key) && manifest[key].ContainsKey("file"))
        {
            var scriptPath = manifest[key]["file"].ToString();
            return new HtmlString($"<script type=\"module\" src=\"/_content/React/{scriptPath}\"></script>");
        }
        return new HtmlString($"<!-- Failed to render {key}-->");
    }

    private static Dictionary<string, Dictionary<string, object>>? GetManifestData(IWebHostEnvironment hostingEnvironment)
    {
        
        var manifestFilePath = hostingEnvironment.IsDevelopment()
            ? Path.Combine(hostingEnvironment.WebRootPath, "../../TemplateProject.WebApi.React/wwwroot", ManifestPath)
            : Path.Combine(hostingEnvironment.WebRootPath, "_content/React", "manifest.json");

        if (File.Exists(manifestFilePath))
        {
            var manifestJson = File.ReadAllText(manifestFilePath);
            return JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(manifestJson);
        }

        return null;
    }
}